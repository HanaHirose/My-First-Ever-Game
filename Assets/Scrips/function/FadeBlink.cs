using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is for make UI blinking with fading.

//To use this, add Canvas Group component to gameobject which you want to make blink,
//and add this script to the gameobject. Drag and drop the gameobject in the box fadeBlinkMyUI.

//This is not affected by time scale.

public class FadeBlink : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeBlinkMyUI;
    [SerializeField] private float periodTime = 3f;

    private bool up = true;


    private void Start()
    {
        fadeBlinkMyUI.alpha = 0f;
    }

    private void Update()
    {
       if (up)
        {
            fadeBlinkMyUI.alpha += Time.unscaledDeltaTime / periodTime;
            if(fadeBlinkMyUI.alpha >= 1)
            {
                up = false;
            }
        }
        else
        {
            fadeBlinkMyUI.alpha -= Time.unscaledDeltaTime / periodTime;
            if(fadeBlinkMyUI.alpha <= 0)
            {
                up = true;
            }
        }
    }
}
