using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragScene : MonoBehaviour {


    public Vector3 m_Offset;
    public Vector3 m_MousePosition;
    public Vector3 m_ObjectPosition;
    public RectTransform m_Bounds;
    public RectTransform m_ThisBounds;


    private Vector3 t_OriginalPosition;

    private void Start() {
        m_ThisBounds = GetComponent<RectTransform>();
        m_Bounds = DragManager.m_Instance.GetComponent<RectTransform>();

        t_OriginalPosition = transform.position;
    }
    private void OnMouseDown() {
        m_Offset = (Vector3)m_ThisBounds.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        m_Offset.z = 0f;
        /*Dragmanager.m_Instance.m_CurrentObject = this.gameObject;
        if(Dragmanager.m_Instance.m_LastObject != this.gameObject && Dragmanager.m_Instance.m_LastObject!=null) {
            Dragmanager.m_Instance.m_LastObject.transform.position = new Vector3(Dragmanager.m_Instance.m_LastObject.transform.position.x, Dragmanager.m_Instance.m_LastObject.transform.position.y, 0f);
        }*/
        transform.SetAsLastSibling();
    }

    private void OnMouseDrag() {
        m_MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        m_ObjectPosition = Camera.main.ScreenToWorldPoint(m_MousePosition);
        transform.position = m_ObjectPosition + m_Offset;
        if ((m_ThisBounds.anchoredPosition.x + m_Offset.x) <= ((-(m_Bounds.sizeDelta.x - m_ThisBounds.sizeDelta.x) / 2))){
            m_ThisBounds.anchoredPosition = new Vector3(((-(m_Bounds.sizeDelta.x - m_ThisBounds.sizeDelta.x) / 2)), m_ThisBounds.anchoredPosition.y, 0f);
        }else if ((m_ThisBounds.anchoredPosition.x + m_Offset.x) >= (((m_Bounds.sizeDelta.x - m_ThisBounds.sizeDelta.x) / 2))) {
            m_ThisBounds.anchoredPosition = new Vector3((((m_Bounds.sizeDelta.x - m_ThisBounds.sizeDelta.x) / 2)), m_ThisBounds.anchoredPosition.y, 0f);
        }

        if ((m_ThisBounds.anchoredPosition.y + m_Offset.y) >= (((m_Bounds.sizeDelta.y - m_ThisBounds.sizeDelta.y) / 2))) {
            m_ThisBounds.anchoredPosition = new Vector3(m_ThisBounds.anchoredPosition.x, (((m_Bounds.sizeDelta.y - m_ThisBounds.sizeDelta.y) / 2)), 0f);
        }else if ((m_ThisBounds.anchoredPosition.y + m_Offset.y) <= ((-(m_Bounds.sizeDelta.y - m_ThisBounds.sizeDelta.y) / 2))) {
            m_ThisBounds.anchoredPosition = new Vector3(m_ThisBounds.anchoredPosition.x, ((-(m_Bounds.sizeDelta.y - m_ThisBounds.sizeDelta.y) / 2)), 0f);
        }
    }
    
    public void f_ResetPosition() { transform.position = t_OriginalPosition; }
}
