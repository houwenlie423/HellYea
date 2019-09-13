using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerScript : MonoBehaviour {

    public static TimerScript Instance;

    public TextMeshProUGUI m_DisplayTxt;
    public float m_WaitTime;
    [HideInInspector] public bool m_IsCountingDown;

    private float m_CurrentTime;
    private int m_CurrHour, m_CurrMinute, m_CurrSecond;
    private Color t_OriginalColor;
    

    private void OnEnable() { 
        Instance = this;
        t_OriginalColor = m_DisplayTxt.color;
    }

    private void FixedUpdate() {
        if(m_IsCountingDown) {
            m_CurrentTime = m_WaitTime;
            m_DisplayTxt.color = t_OriginalColor;
            m_IsCountingDown = false;
        }

        if(m_CurrentTime > 0) {
            m_CurrentTime -= Time.fixedDeltaTime;
            m_CurrHour = (int)m_CurrentTime / 3600;
            m_CurrMinute = (int)(m_CurrentTime % 3600) / 60;
            m_CurrSecond = (int)(m_CurrentTime % 3600) % 60;

            m_DisplayTxt.text = f_DisplayTimer(m_CurrHour, m_CurrMinute, m_CurrSecond);

            if (m_CurrentTime <= 10) m_DisplayTxt.color = Color.red;

            if (m_CurrentTime <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                m_CurrentTime = 0;
            }
        }
    }

    public void f_Begin() { m_IsCountingDown = true; }
    private string f_DisplayTimer(int p_Hour, int p_Minute, int p_Second) { return p_Hour.ToString("00") + ":" + p_Minute.ToString("00") + ":" + p_Second.ToString("00"); }


    /*
     * total sec = 4000 seconds
     *    
     * hours = total sec / 3600 = 4000 / 3600 = 1 hour
     *    
     * remaining_second = total sec % 3600 = 4000 % 3600 = 400
     * minute = remaining_second / 60 = 400 / 60 = 6 mins
     * 
     * remaining_second = remaining_second % 60 = 400%60 = 40
     * 
     * 
     * Conc :
     * hours = total sec / 3600
     * minute = (total sec % 3600) / 60
     * seconds = (total sec % 3600) % 60
     * 
     *
     * */
}
