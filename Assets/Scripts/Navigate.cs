using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaderBoardView()
    {
        SceneManager.LoadScene(3);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
