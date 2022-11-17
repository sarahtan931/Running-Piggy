using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public Text TimerText;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var timeArr = TimerText.text.Split(":");
            var mins = int.Parse(timeArr[0]);
            var seconds = int.Parse(timeArr[1]);
            int timeInt = mins * 60 + seconds;
            PlayerPrefs.SetInt("time", timeInt);
            SceneManager.LoadScene(2);
           
        }
    }
}
