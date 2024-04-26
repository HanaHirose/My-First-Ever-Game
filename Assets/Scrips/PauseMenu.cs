using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    //For button slection
    private GameObject buttonNo;
    private GameObject buttonQuit;

    private PauseTest pt;
    private GameObject pauseTest;
    [SerializeField] private GameObject panel;

    //private bool pauseMenuBool;
    //private bool pBool;

    //private PlayerInputAction playerInputAction;

    //[SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject areYouSureUI;

    //private void Awake()
    //{
    //    //Debug.Log("Awake in PauseMenu script");
    //    playerInputAction = new PlayerInputAction();
    //}

    //private void OnEnable()
    //{
    //    //Debug.Log("OnEnable in PauseMenu script");
    //    playerInputAction.Menu.Enable();
    //}

    //private void OnDisable()
    //{
    //    //Debug.Log("OnDisable in PauseMenu script");
    //    playerInputAction.Menu.Disable();
    //}

    //private void Start()
    //{
    //    //Debug.Log("Start in PauseMenu script");
    //    playerInputAction.Menu.Pause.performed += PauseControl;

    //    //To prevent that sound doesn't appear after go back to main menu from pause and start the game again
    //    //Debug.Log("Set puaseMenu to be false in Start of PauseMenu script");
    //    pauseMenuBool = false;
    //    pBool = false;


    //    //if (!pauseMenuBool)
    //    //{
    //    //    pauseUI.SetActive(true);

    //    //    pauseMenuBool = true;

    //    //}
    //    //else if (pauseMenuBool)
    //    //{
    //    //    pauseUI.SetActive(false);

    //    //    pauseMenuBool = false;

    //    //}


    //}

    //private void Update()
    //{

    //    //if (pauseMenuBool)
    //    //{
    //    //    Debug.Log("mute");
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("unmute");
    //    //}

    //    //pauseUI.SetActive(false);

    //    //if (pauseMenuBool == !pauseMenuBool)
    //    //{
    //    //    if (pauseMenuBool)
    //    //    {
    //    //        //Time.timeScale = 0f;
    //    //        pauseUI.SetActive(true);
    //    //    }
    //    //    else if (!pauseMenuBool)
    //    //    {
    //    //        //Time.timeScale = 1f;
    //    //        pauseUI.SetActive(false);
    //    //    }
    //    //}


    //    Debug.Log("pauseMenuBool: " + pauseMenuBool);
    //    //Debug.Log("pBool: " + pBool);
    //}

    //private void PauseControl(InputAction.CallbackContext context)
    //{
    //    count += 1;
    //    Debug.Log("count: " + count); 

    //    if (!pauseMenuBool)
    //    {
    //        //pauseUI.SetActive(true);
    //        //Time.timeScale = 0f;

    //        pauseMenuBool = true;

    //    }
    //    else if (pauseMenuBool)
    //    {
    //        //pauseUI.SetActive(false);
    //        //Time.timeScale = 1f;

    //        pauseMenuBool = false;

    //    }

    //    //Debug.Log("Pause()");
    //    //Pause();

    //    //if (pauseMenuBool == false)
    //    //{
    //    //    Debug.Log("Pause()");
    //    //    Pause();
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("Resume()");
    //    //    Resume();
    //    //}
    //}

    //private void Pause()
    //{
    //    Debug.Log("Pause with key P");
    //    pauseUI.SetActive(true);
    //    Time.timeScale = 0f;
    //    pauseMenuBool = true;
    //}

    //public void Resume()
    //{
    //    Debug.Log("Resume with key P");
    //    pauseUI.SetActive(false);
    //    Time.timeScale = 1f;
    //    pauseMenuBool = false;
    //}


    private void Start()
    {
        pauseTest = GameObject.Find("PauseTest");
        pt = pauseTest.GetComponent<PauseTest>();
    }


    public void MainMenuButton()
    {
        areYouSureUI.SetActive(true);
        buttonNo = GameObject.Find("ButtonNo");
        EventSystem.current.SetSelectedGameObject(buttonNo);
    }

    public void CloseAreYouSure()
    {
        areYouSureUI.SetActive(false);
        buttonQuit = GameObject.Find("ButtonQuit");
        EventSystem.current.SetSelectedGameObject(buttonQuit);
    }

    public void BackToMainMenu()
    {

        //If you miss the line (Time.timeScale = 1f)
        //to set back the time scalte to 1f,
        //start button in start screen doesn't work
        //after going back to main menu through pause menu.
        //FYI, you don't need to reset them about SetActive UI.
        ////areYouSureUI.SetActive(false);
        ////pauseUI.SetActive(false);
        Time.timeScale = 1f;



        SceneManager.LoadScene("Start screen");
    }

    public void Continue()
    {
        Debug.Log("Continue");
        Debug.Log("Get panelBool in Continue: " + pt.panelBool);
        panel.SetActive(false);
        Time.timeScale = 1f;
        pt.panelBool = false;

    }


}

