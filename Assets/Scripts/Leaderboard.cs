using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    
    void Start()
    {
        var time = PlayerPrefs.GetString("time");
        Debug.Log("time" + time);
    }

 
    void Update()
    {
        
    }
}
