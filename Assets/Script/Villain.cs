using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Villain {
    public string m_FirstName;
    public string m_LastName;
    public char m_Gender;
    public string m_Status;
    public string m_Job;
    public string m_Email;
    public string m_Country;
    public string m_CauseOfDeath;

    public Villain(string p_FirstName, string p_LastName, char p_Gender, string p_Status, string p_Job, string p_Email, string p_Country, string p_CauseOfDeath) {
        m_FirstName = p_FirstName;
        m_LastName = p_LastName;
        m_Gender = p_Gender;
        m_Status = p_Status;
        m_Job = p_Job;
        m_Email = p_Email;
        m_Country = p_Country;
        m_CauseOfDeath = p_CauseOfDeath;
    }
}
