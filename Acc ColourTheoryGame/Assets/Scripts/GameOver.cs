using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;  
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
