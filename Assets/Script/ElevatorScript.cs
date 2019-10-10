using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour {

    public static ElevatorScript Instance;

    private void OnEnable() { Instance = this;}

    public void f_PlayerFadeOut() { UIAnimController.Instance.f_VillainExitAnim(VillainScript.Instance.m_Destination); }

    //AFTER CLOSING DOOR
    public void f_ExitOrNext() {
        Manager.Instance.f_ResetCards();
        if (!TimerScript.Instance.f_TimesUp()) UIAnimController.Instance.f_VillainEntranceAnim();
    }
}
