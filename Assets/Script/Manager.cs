using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Manager : MonoBehaviour{

    public static Manager Instance;

    [Header("List of Purgatory Castes")]
    public PurgatoryCaste[] m_Castes;

    [Header("List of all Sins and Virtues")]
    public List<Sin> m_SinList = new List<Sin>();
    public List<Virtue> m_VirtueList = new List<Virtue>();

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
    public GameObject m_BasicInterface;
    public GameObject m_CasteInterface;
    public GameObject m_IndicatorInterface;
    public TextMeshProUGUI m_SinIndicator;
    public TextMeshProUGUI m_VirtueIndicator;
    public TextMeshProUGUI m_AnnouncementTxt;

    [Header("Developer Helps")]
    public string m_CorrectPlacement;
    public string m_CorrectCaste;
    public int m_CorrectCasteLevel;



    private PurgatoryCaste t_TempCaste;
    private TextAsset m_Database;
    private string[] t_StrLine, t_StrRow;
    private int t_I, t_Rand, t_Count;
    private bool t_Worthy;


    private void OnEnable() { Instance = this; }


    private void Start() {
        m_PlayerScore = 0;
        m_BasicInterface.SetActive(true);
        m_IndicatorInterface.SetActive(true);
        m_CasteInterface.SetActive(false);
        f_Load("ListDosa");
        f_Load("ListPahala");
    }


    private void f_Load(string p_FileName) {
        m_Database = Resources.Load<TextAsset>(p_FileName);
        t_StrLine = m_Database.text.Split('\n');
        for (t_I = 1; t_I < t_StrLine.Length; t_I++) {
            if (t_StrLine[t_I] != "") {
                t_StrRow = t_StrLine[t_I].Split(';');
                if (p_FileName == "ListDosa") m_SinList.Add(new Sin(t_StrRow[0], int.Parse(t_StrRow[1])));
                else if (p_FileName == "ListPahala") m_VirtueList.Add(new Virtue(t_StrRow[0], int.Parse(t_StrRow[1])));
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

    private void f_ResetEverything() {
        m_CorrectPlacement = "";
        m_CorrectCaste = "";
        m_CorrectCasteLevel = 0;
        m_SinIndicator.text = "";
        m_VirtueIndicator.text = "";
        m_AnnouncementTxt.text = "";
        if(m_SinViolated.Count > 0) m_SinViolated.Clear();
        if(m_VirtueCompassed.Count > 0) m_VirtueCompassed.Clear();
        m_CurrVillainRating = 0;
        t_Worthy = false;
    }



    //====================================================================================================================================================================================================================================

    public void f_Generate() {
        //f_ResetEverything();

        //Generate Dosa
        m_SinIndicator.text = "All violations i did in life : \n";
        if (m_SinExtent >= m_SinList.Count) {
            for (t_I = 0; t_I < m_SinList.Count; t_I++) {
                m_SinIndicator.text += t_I + 1 + ". " + m_SinList[t_I].m_Name + "\n";
                m_SinViolated.Add(m_SinList[t_I]);
            }
        }
        else {
            for (t_I = 0; t_I < m_SinExtent; t_I++) {
                do t_Rand = Random.Range(0, m_SinList.Count);
                while (m_SinViolated.Contains(m_SinList[t_Rand]));

                m_SinIndicator.text += t_I + 1 + ". " + m_SinList[t_Rand].m_Name + "\n";
                m_SinViolated.Add(m_SinList[t_Rand]);
            }
        }

        //Generate Pahala
        m_VirtueIndicator.text = "All good deeds i did in life : \n";
        if (m_VirtueExtent >= m_VirtueList.Count) {
            for (t_I = 0; t_I < m_VirtueList.Count; t_I++) {
                m_VirtueIndicator.text += t_I + 1 + ". " + m_VirtueList[t_I].m_Name + "\n";
                m_VirtueCompassed.Add(m_VirtueList[t_I]);
            }
        }
        else {
            for (t_I = 0; t_I < m_VirtueExtent; t_I++) {
                do t_Rand = Random.Range(0, m_VirtueList.Count);
                while (m_VirtueCompassed.Contains(m_VirtueList[t_Rand]));

                m_VirtueIndicator.text += t_I + 1 + ". " + m_VirtueList[t_Rand].m_Name + "\n";
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

        m_SinIndicator.text = "";
        m_VirtueIndicator.text = "";

        if (t_Worthy == p_Submission) m_PlayerScore += 50;


        if (p_Submission) {
            m_BasicInterface.SetActive(false);
            m_CasteInterface.SetActive(true);
        
        }else f_ResetEverything();

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

        m_CasteInterface.SetActive(false);
        m_BasicInterface.SetActive(true);
        f_ResetEverything();
    }

}
