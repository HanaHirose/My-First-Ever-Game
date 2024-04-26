using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseTest : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public bool panelBool;
    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        playerInputAction.Menu.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Menu.Disable();
    }

    private void Start()
    {
        playerInputAction.Menu.Pause.performed += PauseControlQ;

        Debug.Log("Start, panelBool is set false");
        panelBool = false;
    }



    private void Update()
    {
        //Debug.Log("panelBool: " + panelBool);
    }



    private void PauseControlQ(InputAction.CallbackContext context)
    {
        if (panelBool)
        {
            ResumeQ();
        }
        else if (!panelBool)
        {
            PauseQ();
        }

    }

    //Made this public to use this method for Continue button too.
    private void ResumeQ()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        panelBool = false;
    }

    private void PauseQ()
    {
        panelBool = true;
        panel.SetActive(true);
        Time.timeScale = 0f;
        
    }

    //public void Contiune()
    //{
    //    Debug.Log("Continue");
    //    //panel.SetActive(false);
    //    //Time.timeScale = 1f;
    //    //panelBool = false;
    //}

}
