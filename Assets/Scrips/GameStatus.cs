using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//This is sysytem to count Playe Time.

//Note that Time.time start from 0 when app starts to play.

public class GameStatus : MonoBehaviour
{

    static protected float startTime = 0f;
    static protected float finishTime = 0f;
    private float playtime = 0f;
    static protected float playtimeTillPrevious = 0f;

    static protected bool onceDeadBool = false;

    [SerializeField] private TextMeshProUGUI timeText;

    private GameObject finishObject;
    private Finish finish;

    private void Start()
    {
        finishObject = GameObject.Find("Finish");
        finish = finishObject.GetComponent<Finish>();
        Debug.Log("levelCompleted: " + finish.levelCompleted);

        //Record start time if this is the first time of the current level
        if (!onceDeadBool)
        {
            StartTime();
        }
    }


    void Update()
    {
        //playtime = Time.time - startTime;
        //Debug.Log("Time: " + playtime );

        //This is total time to show in screen
        playtime = Time.time - startTime + playtimeTillPrevious;

        string minute = Mathf.Floor(playtime / 60).ToString("00");
        string second = (playtime % 60).ToString("00");

        //timeText.text = "Time: " + playtime;

        //Timer stop to update when you reached Finish.
        //Note that actual timer doesn't stop.
        if (!finish.levelCompleted)
        {
            timeText.text = "Time "+ minute + ":" + second;
        }

        //Debug.Log("once dead?: " + onceDeadBool);
       
    }

    //Use this in StartMenu script to record the time when start the timer.
    //-> Use this in Start() of this script with if statement
    public static void StartTime()
    {
        startTime = Time.time;
    }

    //Use this in Finish script to record the time when touching Finish flag.
    public static void FinishTime()
    {
        finishTime = Time.time;
    }


    //Use this in PlayerDeath script
    public static void MakeTrueOnceDeadBool()
    {
        onceDeadBool = true;
    }

    //Use this in Finish script and StartMenu script
    public static void MakeFalseOnceDeadBool()
    {
        onceDeadBool = false;
    }

    //Do this in Finish script
    public static void PlaytimeTillPrevious()
    {
        playtimeTillPrevious += finishTime - startTime;
    }

    public static float GetPlaytimeTillPrevious()
    {
        return playtimeTillPrevious;
    }

    public static void ResetPlaytimeTillPrevious()
    {
        playtimeTillPrevious = 0f;
    }

}
