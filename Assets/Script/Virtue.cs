using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Virtue {
    public string m_Name;
    public int m_Rating;

    public Virtue(string p_Name, int p_Rating) {
        m_Name = p_Name;
        m_Rating = p_Rating;
    }
}
