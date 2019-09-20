using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimController : MonoBehaviour {

    public static UIAnimController Instance;

    public Animator m_StampEntrAnim;
    public Animator m_AccAnim, m_RejAnim;

    private bool m_StampDisplayed;
   

    private void OnEnable() { 
        Instance = this;
        m_StampDisplayed = false;
    }

    public void f_ToggleStamp() {
        m_StampDisplayed = !m_StampDisplayed;
        m_StampEntrAnim.SetBool("IsDisplayed", m_StampDisplayed);
    }

    public void f_ShowStamp(bool p_Val) {
        m_StampDisplayed = p_Val;
        m_StampEntrAnim.SetBool("IsDisplayed", m_StampDisplayed);
    }

    public void f_Decide(bool p_Decision) {
        if (p_Decision)         m_AccAnim.Play("AcceptAnim");
        else                    m_RejAnim.Play("RejectAnim");
    }
}
