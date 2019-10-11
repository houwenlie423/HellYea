using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillainScript : MonoBehaviour {

    public static VillainScript Instance;

    public bool m_IsTraitor;
    public string m_Destination;
    public RectTransform m_Rect;

    [Header("Components")]
    public Image m_HairImg;
    public Image m_FaceImg;
    public Image m_NeckImg;
    public Image m_BodyImg;
    public Image m_TraitorImg;
    public Sprite[] m_HairList;
    public Sprite[] m_FaceList;
    public Sprite[] m_NeckList;
    public Sprite[] m_BodyList;


    private void OnEnable() { Instance = this; }

    public void f_GenerateCharacter() { //First frame of Entrance Anim (randomize sprites before character shows)

        m_IsTraitor = Random.Range(0, 100) > 50 ? true : false;

        m_HairImg.sprite = m_HairList[Random.Range(0, m_HairList.Length)];
        m_FaceImg.sprite = m_FaceList[Random.Range(0, m_FaceList.Length)];
        m_NeckImg.sprite = m_NeckList[Random.Range(0, m_NeckList.Length)];
        m_BodyImg.sprite = m_BodyList[Random.Range(0, m_BodyList.Length)];

    }

    public void f_StartGame() { Manager.Instance.f_Generate(); } //Last frame of Entrance Anim (generate game after character shows)

    public void f_SetDestination(int p_ValueDest) { m_Destination = p_ValueDest > 0 ? "Heaven" : "Hell"; } //Set Destination before fading out


    //OPEN ELEVATOR AFTER GOING THERE, CLOSE IT AFTER FADING OUT
    public void f_OpenOrCloseElevator(int p_Val) {
        if (m_Destination == "Heaven")              UIAnimController.Instance.f_PlayElevatorAnim(p_Val > 0 ? "Heaven Open" : "Heaven Close");
        else                                        UIAnimController.Instance.f_PlayElevatorAnim(p_Val > 0 ? "Hell Open" : "Hell Close");
        m_TraitorImg.enabled = false;
    }

    public void f_IsTraitor() {
        if (m_IsTraitor) {
            m_TraitorImg.enabled = true;
        }
        else {
            m_TraitorImg.enabled = false;
        }
    }

}
