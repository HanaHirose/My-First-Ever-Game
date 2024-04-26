using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    public bool levelCompleted = false;

    //Should I use public or protected? If protected is used, you need add GetCherriesTillPreviousScene().
    static protected int cherriesTillPreviousScene = 0;

    //This variable is for counting the number of all cherries in all level.
    static protected int cherriesAll = 0;

    private int cherriesInCurrentLevel = 0;

    //call Player object as player
    private GameObject player;
    // call ItemCllector script as ic
    private ItemCllector ic;

    private void Start()
    {

        //Add the number of cherries in the current level
        cherriesInCurrentLevel = GameObject.FindGameObjectsWithTag("Cherry").Length;


        finishSound = GetComponent<AudioSource>();

        //Get player gameobject
        player = GameObject.Find("Player");

        if(player == null)
        {
            Debug.Log("Failded to find an object named 'Player'");
            this.enabled = false;
            return;
        }
        else
        {
            //Debug.Log("player is not null");
            //Debug.Log(player);
        }

        // GetComponent for ic
        ic = player.GetComponent<ItemCllector>();
        //Debug.Log("player.GetComponent<ItemCllector>(): " + player.GetComponent<ItemCllector>());

        if (ic == null)
        {
            Debug.Log("ic is null'");
            return;
        }
        else
        {
            //Debug.Log("ic is not null");
        }

        //Debug.Log("Start, cherriesTillPreviousScnene: " + cherriesTillPreviousScene);
        //Debug.Log("get public cherries, cherries: " + ic.cherries);
    }

    //When you touch the finish flag
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !levelCompleted)
        {
            //Use this from GameStatus script to record the time when touching Finish flag.
            GameStatus.FinishTime();

            //Added the number of cherries in the current level to the variable cherriesAll
            cherriesAll += cherriesInCurrentLevel;

            //Turn back the bool onceDeadBool to false
            GameStatus.MakeFalseOnceDeadBool();

            //Record playtimeTillPrevious
            GameStatus.PlaytimeTillPrevious();

            finishSound.Play();
            levelCompleted = true;
            // call cherries from ItemCollector script
            cherriesTillPreviousScene += ic.cherries;
            Debug.Log("cherriesTillPreviousScene: " + cherriesTillPreviousScene);
            Invoke("CompleteLevel", 2f);
        }
    }

    public static int GetCherriesTillPreviousScene()
    {
        return cherriesTillPreviousScene;
    }

    //This is used in StarMenu script
    public static void ResetCherriesTillPreviousScene()
    {
        cherriesTillPreviousScene = 0;
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static int GetCherriesAll()
    {
        return cherriesAll;
    }

    public static void ResetCherriesAll()
    {
        cherriesAll = 0;
    }

}
