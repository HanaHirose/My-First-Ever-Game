using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIgroup;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    private float speed = 1f;

    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }

    void Update()
    {
        if (fadeIn)
        {
            if (myUIgroup.alpha < 1)
            {
                myUIgroup.alpha += Time.deltaTime * speed;
                if (myUIgroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (myUIgroup.alpha > 0)
            {
                myUIgroup.alpha -= Time.deltaTime * speed;
                if (myUIgroup.alpha <= 0)
                {
                    fadeOut = false;
                }
            }
        }

    }
}
