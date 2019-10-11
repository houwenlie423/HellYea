using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimController : MonoBehaviour {

    public static UIAnimController Instance;

    public Animator m_StampEntrAnim;
    public Animator m_AccAnim, m_RejAnim;
    public Animator m_JerujiAnim;
    public Animator m_VillainSprAnim;
    public Animator m_ElevatorAnim;

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


    public void f_VillainEntranceAnim() { m_VillainSprAnim.Play("Villain Entrance"); }
   public void f_VillainExitAnim(string p_Dest) { m_VillainSprAnim.Play(p_Dest.ToUpper() == "HEAVEN" ? "Villain Exit Heaven" : "Villain Exit Hell"); }


    public void f_MoveVillain(string p_Name) { m_VillainSprAnim.Play(p_Name); }
    public void f_PlayElevatorAnim(string p_Name) { m_ElevatorAnim.Play(p_Name); }

   
}
