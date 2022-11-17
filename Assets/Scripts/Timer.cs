using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float TimePassed;
    public Text TimerText;
 
    void Update()
    {
      
       TimePassed += Time.deltaTime;
       UpdateText(TimePassed);
        
    }

    void UpdateText(float time)
    {
        time += 1;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }
}
