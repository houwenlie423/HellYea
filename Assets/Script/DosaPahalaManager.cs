using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DosaPahalaManager : MonoBehaviour {
    [Header("List Database & Pahala")]
    public List<Dosa> m_ListDosa = new List<Dosa>();
    public List<Pahala> m_ListPahala = new List<Pahala>();

    [Header("List Database & Pahala yang dilakukan")]
    private List<Dosa> m_DosaDone = new List<Dosa>();
    private List<Pahala> m_PahalaDone = new List<Pahala>();

    [Header("Indikator")]
    public TextMeshProUGUI m_DosaTxt;
    public TextMeshProUGUI m_PahalaTxt;
    public TextMeshProUGUI m_AnnouncementTxt;

    [Header("Jumlah Dosa & Pahala yang di generate")]
    public int m_DosaToGenerate;
    public int m_PahalaToGenerate;

    [Header("Rating")]
    public int m_RatingToPass;
    public int m_FinalRating;



    private TextAsset m_Database;
    private string[] t_StrLine, t_StrRow;
    private int t_I, t_Rand, t_Count;
    private bool t_Worthy;


    private void Start() { 
        f_Load("ListDosa");
        f_Load("ListPahala");
    }


    private void f_Load(string p_FileName) {
        m_Database = Resources.Load<TextAsset>(p_FileName);
        t_StrLine = m_Database.text.Split('\n');
        for(t_I = 1; t_I < t_StrLine.Length; t_I++) {
            if(t_StrLine[t_I] != "") {
                t_StrRow = t_StrLine[t_I].Split(';');
                if(p_FileName == "ListDosa")                m_ListDosa.Add(new Dosa(t_StrRow[0], int.Parse(t_StrRow[1])));
                else if (p_FileName == "ListPahala")        m_ListPahala.Add(new Pahala(t_StrRow[0], int.Parse(t_StrRow[1])));
            }
        }
    }

    private int f_Calculate(string p_Act) {
        t_Count = 0;
        for(t_I = 0; t_I < (p_Act == "Dosa" ? m_DosaDone.Count : m_PahalaDone.Count); t_I++)t_Count += p_Act == "Dosa" ? m_DosaDone[t_I].m_Rating : m_PahalaDone[t_I].m_Rating;
        return t_Count;
    }

    private bool f_IsWorthy() {
        m_FinalRating = f_Calculate("Dosa") - f_Calculate("Pahala");
        Debug.Log("Dosa Rating : " + f_Calculate("Dosa") + " , Pahala Rating : " + f_Calculate("Pahala") + ", Final Rating : " + m_FinalRating);
        if (m_FinalRating > m_RatingToPass) return true;
        return false;
    }

    public void f_Generate() {
        m_DosaTxt.text = "Selama Hidup, kejahatan saya : \n\n";
        m_PahalaTxt.text = "Selama Hidup, kebaikan saya : \n\n";
        m_AnnouncementTxt.text = "";

        if (m_DosaDone.Count > 0) m_DosaDone.Clear();
        if (m_PahalaDone.Count > 0) m_PahalaDone.Clear();

        //Generate Dosa
        if (m_DosaToGenerate >= m_ListDosa.Count) {
            for (t_I = 0; t_I < m_ListDosa.Count; t_I++) {
                m_DosaTxt.text += t_I + 1 + ". " + m_ListDosa[t_I].m_NamaDosa + "\n";
                m_DosaDone.Add(m_ListDosa[t_I]);
            }
        }
        else {
            for (t_I = 0; t_I < m_DosaToGenerate; t_I++) {
                do t_Rand = Random.Range(0, m_ListDosa.Count);
                while (m_DosaDone.Contains(m_ListDosa[t_Rand]));

                m_DosaTxt.text += t_I + 1 + ". " + m_ListDosa[t_Rand].m_NamaDosa + "\n";
                m_DosaDone.Add(m_ListDosa[t_Rand]);
            }
        }

        //Generate Pahala
        if (m_PahalaToGenerate >= m_ListPahala.Count) {
            for (t_I = 0; t_I < m_ListPahala.Count; t_I++) {
                m_PahalaTxt.text += t_I + 1 + ". " + m_ListPahala[t_I].m_NamaPahala + "\n";
                m_PahalaDone.Add(m_ListPahala[t_I]);
            }
        }
        else {
            for (t_I = 0; t_I < m_PahalaToGenerate; t_I++) {
                do t_Rand = Random.Range(0, m_ListPahala.Count);
                while (m_PahalaDone.Contains(m_ListPahala[t_Rand]));

                m_PahalaTxt.text += t_I + 1 + ". " + m_ListPahala[t_Rand].m_NamaPahala + "\n";
                m_PahalaDone.Add(m_ListPahala[t_Rand]);
            }
        }
    }

    public void f_ChkAcceptance(bool p_UserSubmission) {
        t_Worthy = f_IsWorthy();
        m_DosaTxt.text = "";
        m_PahalaTxt.text = "";
        m_AnnouncementTxt.text = (p_UserSubmission == t_Worthy ? "BETUL, " : "SALAH, ") + "orang ini masuk " + (t_Worthy ? "Neraka" : "Sorga");
    }
}
