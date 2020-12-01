using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    public int buildIndex;
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(buildIndex);
        
    }
}
