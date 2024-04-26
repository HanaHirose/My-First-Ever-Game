using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Result : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI cherriesResultText;
    [SerializeField] private TextMeshProUGUI timeResultText;
    [SerializeField] private TextMeshProUGUI scoreResultText;
    [SerializeField] private TextMeshProUGUI pointPlusText;
    [SerializeField] private TextMeshProUGUI pointPlusCherriesText;
    [SerializeField] private TextMeshProUGUI pointPlusTimeBonusText;

    [SerializeField] private AudioSource collectibleAudio;

    [SerializeField] private GameObject cherriesUI;
    [SerializeField] private GameObject timeUI;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject cherriesPlusUI;
    [SerializeField] private GameObject timeBonusPlusUI;

    [SerializeField] private GameObject goNextScene;
    [SerializeField] private GameObject pressAnyButtonText;

    [SerializeField] private float cherriesPoints = 100;
    [SerializeField] private float targetTime = 60;
    [SerializeField] private float timePoints = 500;
    [SerializeField] private int number = 3000;

    private float playtime;
    private int cherriesResult;

    private float scoreCherries = 0f;
    private float scoreTimeBonus = 0f;

    private void Start()
    {
        playtime = GameStatus.GetPlaytimeTillPrevious();
        cherriesResult = Finish.GetCherriesTillPreviousScene();

        cherriesResultText.text = cherriesResult + "/" + Finish.GetCherriesAll();

        string minute = Mathf.Floor(playtime / 60).ToString("00");
        string second = (playtime % 60).ToString("00");


        timeResultText.text = minute + ":" + second;

        //Debug.Log("CherriesTillPreviousScene: " + Finish.GetCherriesTillPreviousScene());
        //Debug.Log("CherriesAll: " + Finish.GetCherriesAll());
        //Debug.Log("cherriesAll: " + Finish.GetCherriesAll());

        //pointPlusText.text = "";

        cherriesUI.SetActive(false);
        timeUI.SetActive(false);
        scoreUI.SetActive(false);
        cherriesPlusUI.SetActive(false);
        timeBonusPlusUI.SetActive(false);

        goNextScene.SetActive(false);
        pressAnyButtonText.SetActive(false);

        StartCoroutine(ShowResult());


    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1f);

        collectibleAudio.Play();
        cherriesUI.SetActive(true);

        yield return new WaitForSeconds(1f);

        collectibleAudio.Play();
        timeUI.SetActive(true);

        yield return new WaitForSeconds(1f);

        collectibleAudio.Play();
        scoreUI.SetActive(true);

        StartCoroutine(ShowScore());


        yield break;
    }



    private IEnumerator ShowScore()
    {

        if (ScoreCherries(cherriesResult) > 0f)
        {
            pointPlusCherriesText.text = "+ Cherries point";
            yield return new WaitForSeconds(0.8f);
            cherriesPlusUI.SetActive(true);
            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < number; i++)
            {
                collectibleAudio.Play();
                scoreCherries += ScoreCherries(cherriesResult) / number;

                scoreResultText.text = scoreCherries.ToString("0000");

                yield return new WaitForSeconds(0.05f);

            }
        }
        else
        {
            pointPlusCherriesText.text = "Cherries point 0";
            yield return new WaitForSeconds(0.8f);
            cherriesPlusUI.SetActive(true);
            yield return new WaitForSeconds(0.2f);


            collectibleAudio.Play();
            scoreCherries += ScoreCherries(cherriesResult) / number;

            scoreResultText.text = scoreCherries.ToString("0000");

            yield return new WaitForSeconds(0.5f);
        }


        yield return new WaitForSeconds(0.5f);

        cherriesPlusUI.SetActive(false);



        //Time bonus
        if (ScoreTime(playtime) > 0f)
        {
            pointPlusTimeBonusText.text = "+ Time bonus";
            yield return new WaitForSeconds(0.8f);
            timeBonusPlusUI.SetActive(true);
            yield return new WaitForSeconds(0.2f);


            for (int i = 0; i < number; i++)
            {
                collectibleAudio.Play();
                scoreTimeBonus += ScoreTime(playtime) / number;

                scoreResultText.text = (scoreCherries + scoreTimeBonus).ToString("0000");

                yield return new WaitForSeconds(0.05f);

            }

        }
        else
        {
            pointPlusTimeBonusText.text = "Time bonus 0";

            yield return new WaitForSeconds(0.8f);
            timeBonusPlusUI.SetActive(true);
            yield return new WaitForSeconds(0.2f);

            collectibleAudio.Play();
            scoreTimeBonus += ScoreTime(playtime) / number;

            scoreResultText.text = (scoreCherries + scoreTimeBonus).ToString("0000");

            yield return new WaitForSeconds(0.5f);


        }


        yield return new WaitForSeconds(0.5f);

        timeBonusPlusUI.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        EnableGoNext();

    }


    private void EnableGoNext()
    {
        goNextScene.SetActive(true);
        pressAnyButtonText.SetActive(true);
    }



    private float ScoreCherries(int ch)
    {
        return ch * cherriesPoints;
    }

    private float ScoreTime(float time)
    {
        if (playtime <= targetTime)
        {
            return (targetTime - playtime) / targetTime * timePoints;
        }
        else
        {
            return 0f;
        }
    }
}
