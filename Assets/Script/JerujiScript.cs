using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JerujiScript : MonoBehaviour{

    public void f_EventAfter(int p_Param) { //KARENA ANIMASI EVENT GAK BISA PAKE BOOL, TERPAKSA PAKE INT. 0 = EXIT, 1 = ENTER
        if (p_Param > 0)     Manager.Instance.f_Generate();
        else                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log(p_Param == 0 ? "Exit" : "Entrance");
       
    }

    public void f_StageStart() { Manager.Instance.f_Generate(); }
    public void f_StageOver() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
}
