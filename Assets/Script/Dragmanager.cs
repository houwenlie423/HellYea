using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragmanager : MonoBehaviour
{
    public static Dragmanager m_Instance;
    public GameObject m_CurrentObject;
    public GameObject m_LastObject;

    private void Awake() {
        m_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
