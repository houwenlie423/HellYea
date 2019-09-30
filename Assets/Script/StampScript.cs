using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampScript : MonoBehaviour {

    public Sprite m_StampSprite;
    public Button m_MyBtn;
    public bool m_Acceptance;

    private void OnEnable() { m_MyBtn.interactable = false;}
    private void OnTriggerEnter2D(Collider2D p_Col) {
        if (p_Col.gameObject.tag == "Visa") {
            if (p_Col.gameObject.transform.parent.GetSiblingIndex() == p_Col.gameObject.transform.parent.parent.childCount - 1) m_MyBtn.interactable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D p_Col) {
        if (p_Col.gameObject.tag == "Visa" && m_MyBtn.interactable) {
            if (p_Col.gameObject.transform.parent.GetSiblingIndex() != p_Col.gameObject.transform.parent.parent.childCount - 1) m_MyBtn.interactable = false;
        }
        else if(p_Col.gameObject.tag == "Visa" && !m_MyBtn.interactable) {
            if (p_Col.gameObject.transform.parent.GetSiblingIndex() == p_Col.gameObject.transform.parent.parent.childCount - 1) m_MyBtn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D p_Col) {
        if (p_Col.gameObject.tag == "Visa" && m_MyBtn.interactable) m_MyBtn.interactable = false;
    }
    

    public void f_ChgStamps() {
        Manager.Instance.m_StampImg.sprite = m_StampSprite;
        Manager.Instance.m_StampImg.enabled = true;
        Manager.Instance.f_SubmitAns(m_Acceptance);
        //UIAnimController.Instance.f_ShowStamp(false);
    }
}
