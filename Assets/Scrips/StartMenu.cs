using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    private AudioSource startSound;

    [SerializeField] private GameObject optionUI;
    //[SerializeField] private GameObject startMenuUI;

    //For Button selection
    private GameObject buttonRumble;

    private void Start()
    {
  

        startSound = GetComponent<AudioSource>();

        //Reset the number of cherriesTillPreviousScene in Finish script to be 0
        Finish.ResetCherriesTillPreviousScene();

        //Reset the number of cherriesAll in Finish script to be 0
        Finish.ResetCherriesAll();

        //Turn back the bool onceDeadBool to false
        GameStatus.MakeFalseOnceDeadBool();

        //Reset PlaytimeTillPrevious
        GameStatus.ResetPlaytimeTillPrevious();
    }

    public void StartGame()
    {

        startSound.Play();

        //Record when start the game
        //GameStatus.StartTime();

        Invoke("StartGameNextScene", 1f);
    }

    private void StartGameNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    //public void QuitGame()
    //{
    //    Debug.Log("quit the game");
    //    //Application.Quit();
    //}

    public void Quit()
    {
        Debug.Log("Quit");
        //Application.Quit() function can work only for built one, not for the one playing in editor of Unity.
        Application.Quit();
    }

    public void OpenOption()
    {
        optionUI.SetActive(true);
        //startMenuUI.SetActive(false);

        //For button selection
        buttonRumble = GameObject.Find("ButtonRumbleTest");
        //Debug.Log("button rumble: " + buttonRumble);
        EventSystem.current.SetSelectedGameObject(buttonRumble);

    }
    
    

}
