using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    private float time = 0f;
    private bool goNextBool = false;
    private FadeController fadeController;

    [SerializeField] private bool goNextWithTime = false;
    [SerializeField] private float timeGoNext = 20f;


    private void Start()
    {
        fadeController = GetComponent<FadeController>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        //Make this active if you want the scene to go to the next after certain time passes.
        //-----

        if (goNextWithTime)
        {
            if (time >= timeGoNext)
            {
                goNextBool = true;
            }
        }

        //-----

        //Input.anyKeyDown includes any key button of keyboard and buttons of gamepad,
        //not including joysticks and arrow keys of gamepad
        if (Input.anyKeyDown || goNextBool)
        {
            //Fade out.
            //To use this, you need to add FadeController script to this gameobject.
            //(Or you need to create new gameobject and add FadeController script to the object,
            //and do like fadescript = GameObject.Find("FadeScript");)
            fadeController.HideUI();


            Invoke("GoBackToStartScene", 2f);
            //GoBackToStartScene();

        }
    }

    private void GoBackToStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
