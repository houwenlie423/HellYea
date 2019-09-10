using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DosaManager : MonoBehaviour{
    public List<Dosa> m_ListDosa = new List<Dosa>();
    public TextMeshProUGUI m_DosaTxt;
    public TextAsset m_DosaDbs;
    public int m_RatingToPass;
    public int m_TotalRating;

    private List<Dosa> m_DosaDone = new List<Dosa>();
    private string[] t_DosaStr, t_DosaRow;
    private int t_I, t_Rand;
    private bool t_Worthy;


    private void Start() { f_LoadDosa();}

    private void f_LoadDosa() {
        m_DosaDbs = Resources.Load<TextAsset>("ListDosa");
        t_DosaStr = m_DosaDbs.text.Split('\n');

        for(t_I = 1; t_I < t_DosaStr.Length; t_I++) {
            if(t_DosaStr[t_I] != "") {
                t_DosaRow = t_DosaStr[t_I].Split(';');
                m_ListDosa.Add(new Dosa(t_DosaRow[0], int.Parse(t_DosaRow[1])));
            }
        }
    }

    private bool f_IsWorthy() {
        m_TotalRating = 0;
        for (t_I = 0; t_I < m_DosaDone.Count; t_I++) m_TotalRating += m_DosaDone[t_I].m_Rating;
        if (m_TotalRating > m_RatingToPass) return true;
        return false;
    }

    public void f_GenerateDosa(int p_Count) {
        m_DosaTxt.text = "Selama Hidup, saya : \n\n";
        m_DosaDone.Clear();

        if(p_Count >= m_ListDosa.Count) {
            for(t_I = 0; t_I < m_ListDosa.Count; t_I++) {
                m_DosaTxt.text += t_I+1 + ". " + m_ListDosa[t_I].m_NamaDosa + "\n";
                m_DosaDone.Add(m_ListDosa[t_I]);
            }
        }else {
            for(t_I = 0; t_I < p_Count; t_I ++) {
                do t_Rand = Random.Range(0, m_ListDosa.Count);
                while (m_DosaDone.Contains(m_ListDosa[t_Rand]));

                m_DosaTxt.text += t_I + 1 + ". " + m_ListDosa[t_Rand].m_NamaDosa + "\n";
                m_DosaDone.Add(m_ListDosa[t_Rand]);
            }
        }
    }

    public void f_ChkAcceptance(bool p_UserSubmission) {
        t_Worthy = f_IsWorthy();
        m_DosaTxt.text = (p_UserSubmission == t_Worthy ? "BETUL, " : "SALAH, ") + "orang ini masuk " + (t_Worthy ? "Neraka" : "Sorga");
    }
}
