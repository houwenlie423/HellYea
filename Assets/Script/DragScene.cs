using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScene : MonoBehaviour {
    public Vector3 m_Offset;
    public Vector3 m_MousePosition;
    public Vector3 m_ObjectPosition;
    public SpriteRenderer m_Bounds;
    public SpriteRenderer m_ThisBounds;
    bool m_Drag;
    private void Start() {
        m_ThisBounds = GetComponent<SpriteRenderer>();
        m_Bounds = Dragmanager.m_Instance.GetComponent<SpriteRenderer>();
    }
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
        transform.position = new Vector3(
            Mathf.Clamp(m_ObjectPosition.x + m_Offset.x, 
            (-(m_Bounds.bounds.size.x - m_ThisBounds.bounds.size.x) / 2) + m_Bounds.transform.position.x,
            ((m_Bounds.bounds.size.x - m_ThisBounds.bounds.size.x) / 2) + m_Bounds.transform.position.x), 
            Mathf.Clamp(m_ObjectPosition.y + m_Offset.y,
            (-(m_Bounds.bounds.size.y - m_ThisBounds.bounds.size.y) / 2) + m_Bounds.transform.position.y,
            ((m_Bounds.bounds.size.y - m_ThisBounds.bounds.size.y) / 2) + m_Bounds.transform.position.y), 
            m_ObjectPosition.z + m_Offset.z
            );
    }

    private void OnMouseUp() {
        Dragmanager.m_Instance.m_LastObject = this.gameObject;
    }
    private void Update() {
      
    }
}
