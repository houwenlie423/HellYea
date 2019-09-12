using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Manager : MonoBehaviour{

    public static Manager Instance;

    [Header("Purgatory Castes")]
    public PurgatoryCaste[] m_Castes;

    [Header("Database")]
    public List<Sin> m_SinList = new List<Sin>();
    public List<Virtue> m_VirtueList = new List<Virtue>();
    public List<Villain> m_VillainList = new List<Villain>();

    [Header("Sins and Virtues done by each specific villain")]
    private List<Sin> m_SinViolated = new List<Sin>();
    private List<Virtue> m_VirtueCompassed = new List<Virtue>();

    [Header("Amounts of Sins and Virtues to be generated")]
    public int m_SinExtent;
    public int m_VirtueExtent;

    [Header("Rating")]
    public int m_RatingToPass;
    public int m_CurrVillainRating;
    public int m_PlayerScore;

    [Header("Interfaces")]
    public Button m_StartBtn;
    public GameObject m_InGameUI;
    public TextMeshProUGUI m_TimerTxt;
    public GameObject m_SpriteVillain;
    public GameObject m_SpriteTable;
    public Button m_DecideBtn;
    public Button m_GuideBtn;
    public GameObject m_GuideBook;
    public GameObject m_AcceptOrReject;
    public GameObject m_CasteChoices;
    //JGN DIUBAH2 DLU
    public TextMeshProUGUI m_SinDocument;
    public TextMeshProUGUI m_VirtueDocument;
    public TextMeshProUGUI m_VillainIdentity;
    public TextMeshProUGUI m_JobsDoneTxt;


    [Header("Developer Helps")]
    public string m_CorrectPlacement;
    public string m_CorrectCaste;
    public int m_CorrectCasteLevel;


    private int m_JobsDone;
    private Villain t_PrevVillain;
    private PurgatoryCaste t_TempCaste;
    private TextAsset m_Database;
    private string[] t_StrLine, t_StrRow;
    private int t_I, t_Rand, t_Count;
    private bool t_Worthy;
    private GameObject[] m_ObjArr = new GameObject[10];

    private void OnEnable() { Instance = this; }


    private void Start() {
        m_PlayerScore = 0;
        m_JobsDone = 0;
        m_JobsDoneTxt.text = "Villains taken care of " + m_JobsDone;
        t_PrevVillain = null;

        //Beginning UI
        m_StartBtn.gameObject.SetActive(true);
        m_InGameUI.gameObject.SetActive(false);

        f_Load("listPeople");
        f_Load("ListDosa");
        f_Load("ListPahala");
    }


    private void f_Load(string p_FileName) {
        m_Database = Resources.Load<TextAsset>(p_FileName);
        t_StrLine = m_Database.text.Split('\n');
        for (t_I = 1; t_I < t_StrLine.Length; t_I++) {
            if (t_StrLine[t_I] != "") {
                t_StrRow = t_StrLine[t_I].Split(';');
                if (p_FileName == "ListDosa")           m_SinList.Add(new Sin(t_StrRow[0], int.Parse(t_StrRow[1])));
                else if (p_FileName == "ListPahala")    m_VirtueList.Add(new Virtue(t_StrRow[0], int.Parse(t_StrRow[1])));
                else if (p_FileName == "listPeople")    m_VillainList.Add(new Villain(t_StrRow[0], t_StrRow[1], t_StrRow[2], t_StrRow[3], t_StrRow[4], t_StrRow[5], t_StrRow[6], t_StrRow[7], t_StrRow[8]));
            }
        }
    }

    private void f_CalculateRating() {
        m_CurrVillainRating = 0;
        for (t_I = 0; t_I < m_SinViolated.Count; t_I++) m_CurrVillainRating += m_SinViolated[t_I].m_Rating;
        for (t_I = 0; t_I < m_VirtueCompassed.Count; t_I++) m_CurrVillainRating -= m_VirtueCompassed[t_I].m_Rating;
    }

    private bool f_IsWorthy() {
        f_CalculateRating();
        if (m_CurrVillainRating > m_RatingToPass) return true;
        return false;
    }

    private PurgatoryCaste f_DetermineCaste() {
        if (!t_Worthy) return null;
        for(t_I = 0; t_I < m_Castes.Length; t_I++) if (m_CurrVillainRating > m_Castes[t_I].m_RequiredPoint && m_CurrVillainRating <= m_Castes[t_I].m_MaxPoint) return m_Castes[t_I];
        return null;
    }

    private void f_ResetValues() {
        m_CorrectPlacement = "";
        m_CorrectCaste = "";
        m_CorrectCasteLevel = 0;

        if(m_SinViolated.Count > 0) m_SinViolated.Clear();
        if(m_VirtueCompassed.Count > 0) m_VirtueCompassed.Clear();

        m_CurrVillainRating = 0;
        t_Worthy = false;
    }

    private void f_ResetUI() {
        m_InGameUI.gameObject.SetActive(true);
        m_StartBtn.gameObject.SetActive(false);
        m_TimerTxt.gameObject.SetActive(true);
        m_SpriteVillain.gameObject.SetActive(true);
        m_SpriteTable.gameObject.SetActive(true);
        m_JobsDoneTxt.gameObject.SetActive(true);
        m_DecideBtn.gameObject.SetActive(true);
        m_SinDocument.gameObject.SetActive(true);
        m_VirtueDocument.gameObject.SetActive(true);
        m_VillainIdentity.gameObject.SetActive(true);
        m_GuideBtn.gameObject.SetActive(true);
        m_GuideBook.gameObject.SetActive(false);
        m_AcceptOrReject.gameObject.SetActive(false);
        m_CasteChoices.gameObject.SetActive(false);


        m_SinDocument.text = "";
        m_VirtueDocument.text = "";
        m_VillainIdentity.text = "";
    }


    //====================================================================================================================================================================================================================================

    public void f_StartGame() { f_Generate();}

    public void f_ToggleDecide() {
        //Decide Btn toggle
        m_SinDocument.gameObject.SetActive(!m_SinDocument.gameObject.activeSelf);
        m_VirtueDocument.gameObject.SetActive(!m_VirtueDocument.gameObject.activeSelf);
        m_VillainIdentity.gameObject.SetActive(!m_VillainIdentity.gameObject.activeSelf);
        m_AcceptOrReject.gameObject.SetActive(!m_AcceptOrReject.gameObject.activeSelf);
    }

    public void f_ToggleGuideBook() { m_GuideBook.gameObject.SetActive(!m_GuideBook.gameObject.activeSelf); }

    public void f_Generate() {
        f_ResetValues();
        f_ResetUI();

        //Generate Villain's Identity
        m_VillainIdentity.text = "Vilain's Identity : \n";
        do t_Rand = Random.Range(0, m_VillainList.Count);
        while (t_PrevVillain != null && t_PrevVillain == m_VillainList[t_Rand]);
        t_PrevVillain = m_VillainList[t_Rand];
        m_VillainIdentity.text += "ID : " + m_VillainList[t_Rand].m_ID + "\n";
        m_VillainIdentity.text += "Name : " + m_VillainList[t_Rand].m_FirstName + " " + m_VillainList[t_Rand].m_LastName + "\n";
        m_VillainIdentity.text += "Gender : " + m_VillainList[t_Rand].m_Gender + "\n";
        m_VillainIdentity.text += "Status : " + m_VillainList[t_Rand].m_Status + "\n";
        m_VillainIdentity.text += "Job : " + m_VillainList[t_Rand].m_Job + "\n";
        m_VillainIdentity.text += "Email : " + m_VillainList[t_Rand].m_Email + "\n";
        m_VillainIdentity.text += "Country : " + m_VillainList[t_Rand].m_Country + "\n";
        m_VillainIdentity.text += "Cause of Death : " + m_VillainList[t_Rand].m_CauseOfDeath + "\n";



        //Generate Dosa
        m_SinDocument.text = "All violations i did in life : \n";
        if (m_SinExtent >= m_SinList.Count) {
            for (t_I = 0; t_I < m_SinList.Count; t_I++) {
                m_SinDocument.text += t_I + 1 + ". " + m_SinList[t_I].m_Name + "\n";
                m_SinViolated.Add(m_SinList[t_I]);
            }
        }
        else {
            for (t_I = 0; t_I < m_SinExtent; t_I++) {
                do t_Rand = Random.Range(0, m_SinList.Count);
                while (m_SinViolated.Contains(m_SinList[t_Rand]));

                m_SinDocument.text += t_I + 1 + ". " + m_SinList[t_Rand].m_Name + "\n";
                m_SinViolated.Add(m_SinList[t_Rand]);
            }
        }

        //Generate Pahala
        m_VirtueDocument.text = "All good deeds i did in life : \n";
        if (m_VirtueExtent >= m_VirtueList.Count) {
            for (t_I = 0; t_I < m_VirtueList.Count; t_I++) {
                m_VirtueDocument.text += t_I + 1 + ". " + m_VirtueList[t_I].m_Name + "\n";
                m_VirtueCompassed.Add(m_VirtueList[t_I]);
            }
        }
        else {
            for (t_I = 0; t_I < m_VirtueExtent; t_I++) {
                do t_Rand = Random.Range(0, m_VirtueList.Count);
                while (m_VirtueCompassed.Contains(m_VirtueList[t_Rand]));

                m_VirtueDocument.text += t_I + 1 + ". " + m_VirtueList[t_Rand].m_Name + "\n";
                m_VirtueCompassed.Add(m_VirtueList[t_Rand]);
            }
        }

        //DEVELOPER'S HELP
        t_Worthy = f_IsWorthy();
        t_TempCaste = f_DetermineCaste();
        m_CorrectPlacement = t_Worthy ? "Hell" : "Paradise";
        if(t_TempCaste != null) {
            m_CorrectCaste = t_TempCaste.m_CasteName;
            m_CorrectCasteLevel = t_TempCaste.m_CasteLevel;
        }else {
            m_CorrectCaste = "None";
            m_CorrectCasteLevel = -1;
        }

    }

    public void f_ChkAcceptance(bool p_Submission) {
        //t_Worthy = f_IsWorthy(); alr done above. Uncomment this once DEVELOPER'S HELP is no longer needed

        m_SinDocument.text = "";
        m_VirtueDocument.text = "";
        m_VillainIdentity.text = "";

        if (t_Worthy == p_Submission) m_PlayerScore += 50;


        if (p_Submission) { //SUBMIT ACCEPT
            m_SinDocument.gameObject.SetActive(false);
            m_VirtueDocument.gameObject.SetActive(false);
            m_VillainIdentity.gameObject.SetActive(false);
            m_CasteChoices.gameObject.SetActive(true);

        }else { //SUBMIT REJECT
            m_JobsDone++;
            m_JobsDoneTxt.text = "Villains taken care of " + m_JobsDone;
            f_Generate();
        }

    }

    public void f_ChkCaste(int p_Submission) {
        //t_TempCaste = f_DetermineCaste(); alr done above. Uncomment this once DEVELOPER'S HELP is no longer needed

        if (t_TempCaste != null) {
            m_CorrectCaste = t_TempCaste.m_CasteName;
            m_CorrectCasteLevel = t_TempCaste.m_CasteLevel;

            switch (Mathf.Abs(t_TempCaste.m_CasteLevel - p_Submission)) {
                case 0: m_PlayerScore += 50; break;
                case 1: m_PlayerScore += 40; break;
                case 2: m_PlayerScore += 30; break;
                case 3: m_PlayerScore += 20; break;
                case 4: m_PlayerScore += 10; break;
            }

        }


        m_JobsDone++;
        m_JobsDoneTxt.text = "Villains taken care of " + m_JobsDone;
        f_Generate();
    }

}


/*
 * BEGINNING - 
 *      1. Start Button active DONE
 *      2. Everything else inactive DONE
 * 
 * START ISSUED
 *      1. Start button inactive DONE
 *      2. Timer active DONE
 *      3. Villain sprite & table sprite active DONE
 *      4. Jobs done active DONE
 *      5. DecideBtn active DONE
 *      6. Sin document active DONE
 *      7. Virtue document active DONE 
 *      8. Villain indentity active DONE
 *      8. Guide book button active DONE
 *      9. Guide panel inactive DONE
 *      10. Accept button inactive DONE
 *      11. Reject button inactive DONE
 *      12. Caste choices inactive DONE
 *      13. Generate new
 * 
 * DECIDE BTN TOGGLED
 *      1. Toggle setactive Sin Documet DONE 
 *      2. Toggle setactive Virtue Document DONE
 *      3. Toggle setactive Villain identity DONE
 *      4. Toggle setactive Accept button DONE
 *      5. Toggle setactive Reject Button DONE
 * 
 * ACCEPT BTN ISSUED
 *      1. Sin document inactive DONE
 *      2. Virtue document inactive DONE
 *      3. Villain identity inactive DONE
 *      4. Caste Choices active DONE
 *      5. ????
 * 
 * CASTE DECIDED 
 *      1. Back to Start
 *      2. Generate new
 * 
 * REJECT BTN ISSUED
 *      1. Back to Start
 *      2. Generate new
 * 
 * GUIDE BOOK TOGGLE
 *      1. Toggle Guide Panel
 *      2. ??????????
*/