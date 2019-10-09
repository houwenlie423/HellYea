using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimController : MonoBehaviour {

    public static UIAnimController Instance;

    public Animator m_StampEntrAnim;
    public Animator m_AccAnim, m_RejAnim;
    public Animator m_JerujiAnim;
    public Animator m_VillainSprAnim;

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

    public void f_ShowJeruji(bool p_IsStageRunning) { m_JerujiAnim.Play(p_IsStageRunning ? "JerujiExit" : "JerujiEntrance"); }

    public void f_PlayVillainAnim(bool p_Enter) { m_VillainSprAnim.Play(p_Enter ? "Villain Entrance" : "Villain Exit"); }
}
