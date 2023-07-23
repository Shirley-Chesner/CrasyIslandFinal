using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }
}
