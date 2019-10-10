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

    public void f_StageStart() { UIAnimController.Instance.f_VillainEntranceAnim(); }
    public void f_StageOver() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    /*
     * 1. Start Button
     * 2. Jeruji Exit
     * 3. Randomize Character (set active false)
     * 4. Villain Entrance
     * 5. f_Generate()
     * 6. Game
     * 7. IF submit ans = accept
     *      - Choose caste
     *      - TO HELL
     *      - Open HELL DOOR
     *      - Character Fade Out   
     *      - Close HELL DOOR
     *      - Randomize Character (setactive false)
     *      - Villain Entrance
     *  
     *   ELSE 
     *      - To HEAVEN
     *      - Open HEAVEN Door
     *      - Character Fade Out   
     *      - Close Heaven DOOR
     *      - Randomize Character (setactive false)
     *      - Villain Entrance
     * 
     * 8. IF Time is up
     *      - Jeruji Entrance
     *      - Reload Scene   
     * 
     * */
}
