using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTriger : MonoBehaviour
{
    public Slider slider;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Display")
        {
            slider.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player Display")
        {
            slider.gameObject.SetActive(false);
        }
    }
}
