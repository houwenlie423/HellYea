using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildDrag : MonoBehaviour {
    //rumus cari lebar object skala kecil = ukuran besar * (ukuran meja kecil / ukuran meja besar)
    public RectTransform m_ThisPosition;
    public RectTransform m_OriginalPosition;
    public float m_PositionX;
    public float m_PositionY;
    public RectTransform m_SmallTable;
    public RectTransform m_LargeTable;

    // Start is called before the first frame update
    void Start() {
        m_LargeTable = DragManager.m_Instance.GetComponent<RectTransform>();
        m_ThisPosition = GetComponent<RectTransform>();
        m_ThisPosition.sizeDelta = new Vector2(m_OriginalPosition.sizeDelta.x * (m_SmallTable.sizeDelta.x / m_LargeTable.sizeDelta.x), m_OriginalPosition.sizeDelta.y * (m_SmallTable.sizeDelta.y / m_LargeTable.sizeDelta.y));
        transform.localPosition = new Vector3(f_GetPos(m_OriginalPosition.localPosition.x, m_SmallTable.sizeDelta.x, m_LargeTable.sizeDelta.x), f_GetPos(m_OriginalPosition.localPosition.y, m_SmallTable.sizeDelta.y, m_LargeTable.sizeDelta.y), transform.localPosition.z);
    }

    // Update is called once per frame
    void Update() {
        m_PositionX = f_GetPos(m_OriginalPosition.localPosition.x, m_SmallTable.sizeDelta.x, m_LargeTable.sizeDelta.x);
        m_PositionY = f_GetPos(m_OriginalPosition.localPosition.y, m_SmallTable.sizeDelta.y, m_LargeTable.sizeDelta.y);
        transform.localPosition = new Vector3(m_PositionX, m_PositionY, 0f);
    }

    float f_GetPos(float p_Pos, float p_MapSize, float p_SceneSize) {
        return p_Pos * (p_MapSize/p_SceneSize);
    }

   


}





