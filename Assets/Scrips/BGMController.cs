using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    private PauseTest pt;
    private GameObject pauseTest;


    private void Start()
    {
        pauseTest = GameObject.Find("PauseTest");

        if(pauseTest != null)
        {
            pt = pauseTest.GetComponent<PauseTest>();

        }


        bgm.mute = false;
    }

    private void Update()
    {
        if(pauseTest != null)
        {
            if (pt.panelBool)
            {
                bgm.mute = true;
            }
            else if (!pt.panelBool)
            {
                bgm.mute = false;
            }


        }
        //Debug.Log("BGM :" + pt.panelBool);

        //if (PauseMenu.pauseMenuBool)
        //{
        //    Debug.Log("mute");
        //    bgm.mute = true;
        //}
        //else
        //{
        //    Debug.Log("unmute");
        //    bgm.mute = false;
        //}


        //if (PauseMenu.pauseMenu)
        //{
        //    Debug.Log("pauseMenu is true");
        //    bgm.mute = true;
        //}
        //else if(!PauseMenu.pauseMenu)
        //{
        //    Debug.Log("pauseMenu is false");
        //    bgm.mute = false;
        //}
        //else
        //{
        //    Debug.Log("pauseMenu is null!");
        //}

        //Debug.Log("pauseMenu: " + PauseMenu.pauseMenu);


        //if (PauseMenu.pauseChangeTo == !PauseMenu.pauseChangeTo)
        //{
        //    if (PauseMenu.pauseChangeTo)
        //    {
        //        Debug.Log("mute");
        //        //bgm.mute = !bgm.mute;
        //    }
        //    else
        //    {
        //        Debug.Log("unmute");

        //        //bgm.mute = !bgm.mute;
        //    }
        //}

    }


}
