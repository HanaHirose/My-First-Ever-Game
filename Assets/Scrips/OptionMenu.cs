using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class OptionMenu : MonoBehaviour
{
    //For button selection
    private GameObject optionButton;

    [SerializeField] private GameObject optionUI;
    //[SerializeField] private GameObject startMenuUI;

    private Gamepad gamepad;


    //eventSystem is for controlling selected state of rumble button
    private EventSystem eventSystem;


    private void Start()
    {
        eventSystem = EventSystem.current;
    }


    public void CloseOption()
    {
        //startMenuUI.SetActive(true);
        optionUI.SetActive(false);

        optionButton = GameObject.Find("ButtonOption");
        EventSystem.current.SetSelectedGameObject(optionButton);
       
    }

    public void RumbleTest()
    {
        gamepad = Gamepad.current;

        if(gamepad != null)
        {
            Debug.Log("gamepad: " + gamepad);
            Debug.Log("Start Rumble");
            gamepad.SetMotorSpeeds(1f, 1f);
            Invoke(nameof(StopRumble), 1f);
        }
        else
        {
            Debug.Log("gamepad is null");
        }

        //Invoke(nameof(UnSelected), 1f);

    }

    private void StopRumble()
    {
        Debug.Log("Stop rumble");
        gamepad.SetMotorSpeeds(0f, 0f);
    }

    private void UnSelected()
    {
        eventSystem.SetSelectedGameObject(null);
    }

}
