using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutToNext : MonoBehaviour
{

    private FadeController fadeController;
    private GameObject fadescript;

    private float time = 0f;
    [SerializeField] private float timeFadeOut = 7f;
    private bool fadeBool = false;

    private void Start()
    {
        //fadeController = new FadeController();
        fadescript = GameObject.Find("FadeScript");
        fadeController = fadescript.GetComponent<FadeController>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time >= timeFadeOut)
        {
            fadeBool = true;
        }

        //Input.anyKeyDown includes any key button of keyboard and buttons of gamepad,
        //not including joysticks and arrow keys of gamepad
        if (Input.anyKeyDown || fadeBool)
        {
            Debug.Log("FadeOut starts");
            fadeController.HideUI();
            Invoke("GoBackToStartScene", 3f);
            //GoBackToStartScene();
            
        }
    }

    private void GoBackToStartScene()
    {
        SceneManager.LoadScene("Start screen");
    }

}
