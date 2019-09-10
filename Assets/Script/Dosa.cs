using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dosa 
{
    public string m_NamaDosa;
    public int m_Rating;

    public Dosa(string p_NamaDosa, int p_Rating) {
        m_NamaDosa = p_NamaDosa;
        m_Rating = p_Rating;
    }
}
