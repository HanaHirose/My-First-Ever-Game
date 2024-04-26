using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EscapeSystem : MonoBehaviour
{
    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        //Debug.Log("Awake in EscapeSystem script");
        playerInputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        //Debug.Log("OnEnable in EscapeSystem script");
        playerInputAction.Menu.Enable();
    }

    private void OnDisable()
    {
        //Debug.Log("OnDisable in EscapeSystem script");
        playerInputAction.Menu.Disable();
    }

    private void Start()
    {
        //Debug.Log("Start in EscapeSystem script");
        playerInputAction.Menu.QuitGame.performed += QuitGame;
    }

    private void QuitGame(InputAction.CallbackContext context)
    {
        Debug.Log("Quit game with escape key");
        Application.Quit();
    }

}
