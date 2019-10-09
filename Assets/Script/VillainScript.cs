using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillainScript : MonoBehaviour {

    public static VillainScript Instance;

    public Image m_HairImg;
    public Image m_FaceImg;
    public Image m_NeckImg;
    public Image m_BodyImg;

    public Sprite[] m_HairList;
    public Sprite[] m_FaceList;
    public Sprite[] m_NeckList;
    public Sprite[] m_BodyList;


    private void OnEnable() { Instance = this; }

    public void f_GenerateCharacter() { //First frame of Entrance Anim (randomize sprites before character shows)
        m_HairImg.sprite = m_HairList[Random.Range(0, m_HairList.Length)];
        m_FaceImg.sprite = m_FaceList[Random.Range(0, m_FaceList.Length)];
        m_NeckImg.sprite = m_NeckList[Random.Range(0, m_NeckList.Length)];
        m_BodyImg.sprite = m_BodyList[Random.Range(0, m_BodyList.Length)];
    }

    public void f_StartGame() { Manager.Instance.f_Generate(); } //Last frame of Entrance Anim (generate game after character shows)


    //Last frame of Exit Anim (re-ranzom char and show it again if timer is not up)
    public void f_ExitOrReappear() {
        Manager.Instance.f_ResetCards();
        if (!TimerScript.Instance.f_TimesUp()) UIAnimController.Instance.f_PlayVillainAnim(true);
    } 

}
