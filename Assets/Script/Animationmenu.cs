using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationmenu : MonoBehaviour
{
    public GameObject m_Object;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void f_TurnOff() {
        this.gameObject.SetActive(false);
    }
    public void f_Test() {

        m_Object.SetActive(true);
    }
}
