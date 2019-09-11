using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScene : MonoBehaviour {
    public Vector3 m_Offset;
    public Vector3 m_MousePosition;
    public Vector3 m_ObjectPosition;
    bool m_Drag;
    private void OnMouseDown() {
        m_Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z)));
        m_Offset.z = -1f;
        Dragmanager.m_Instance.m_CurrentObject = this.gameObject;
        if(Dragmanager.m_Instance.m_LastObject != this.gameObject && Dragmanager.m_Instance.m_LastObject!=null) {
            Dragmanager.m_Instance.m_LastObject.transform.position = new Vector3(Dragmanager.m_Instance.m_LastObject.transform.position.x, Dragmanager.m_Instance.m_LastObject.transform.position.y, 0f);

        }
    }

    private void OnMouseDrag() {
        m_MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z));
        m_ObjectPosition = Camera.main.ScreenToWorldPoint(m_MousePosition);
        m_ObjectPosition.z = 0f;
        transform.position = m_ObjectPosition + m_Offset;
        
    }

    private void OnMouseUp() {
        Dragmanager.m_Instance.m_LastObject = this.gameObject;
    }
    private void Update() {
      
    }
}
