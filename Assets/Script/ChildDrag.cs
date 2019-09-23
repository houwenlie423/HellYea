using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildDrag : MonoBehaviour {
    public Vector3 m_ThisPosition;
    public RectTransform m_OriginalPosition;
    public RectTransform m_ThisPos;
    public float m_OffsetX;
    public float m_OffsetY;
    public float m_OffSetX;
    public float m_OffSetY;
    public float m_OffSetRectY;
    public float m_OffSetRectX;
    public float m_PlayerX;
    public float m_PlayerY;
    public float m_MapWidth;
    public float m_MapHeight;
    public RectTransform m_Scene;
    // Start is called before the first frame update
    void Start() {
        m_Scene = DragManager.m_Instance.GetComponent<RectTransform>();
        m_ThisPos = GetComponent<RectTransform>();
        m_OffSetX = transform.position.x - m_OriginalPosition.position.x;
        m_OffSetY = transform.position.y - m_OriginalPosition.position.y;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(transform.position);
        m_OffsetX = m_OriginalPosition.position.x - transform.position.x;
        m_OffsetY = m_OriginalPosition.position.y - transform.position.y;
        m_PlayerX = f_GetPos(transform.position.x, m_OffsetX, m_MapWidth, m_Scene.sizeDelta.x);
        m_PlayerY = f_GetPos(transform.position.y, m_OffsetY, m_MapHeight, m_Scene.sizeDelta.y);
        transform.position = new Vector3(m_PlayerX + m_OffSetX, m_PlayerY + m_OffSetY, 0f);
        m_ThisPos.anchoredPosition = new Vector2(m_ThisPos.anchoredPosition.x + m_OffSetRectX, m_ThisPos.anchoredPosition.y + m_OffSetRectY);
    }

    float f_GetPos(float p_Pos,float p_Offset, float p_MapSize, float p_SceneSize) {
        return (p_Pos + p_Offset) * p_MapSize/p_SceneSize;
    }

   


}





