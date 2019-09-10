using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pahala {
    public string m_NamaPahala;
    public int m_Rating;

    public Pahala(string p_NamaPahala, int p_Rating) {
        m_NamaPahala = p_NamaPahala;
        m_Rating = p_Rating;
    }
}
