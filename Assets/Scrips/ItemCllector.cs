using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class ItemCllector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cherriesText;
    [SerializeField] private AudioSource collectSoundEffect;

    // made cherries public to access from other scripts
    //cherries is the number of cherries that you collect in the current scene.
    //It is reset to be 0 when you die.
    public int cherries = 0;

    //currentCherries is the number of cherries to show in screen
    private int currentCherries = 0;

    //private GameObject fin;
    //private Finish finishScript;

    private void Start()
    {
        //To show text for the first moment just after a scence turned to be the next one, not 0 but a correct number of cherries
        currentCherries = cherries + Finish.GetCherriesTillPreviousScene();
        cherriesText.text = "Cherries: " + currentCherries;

        //fin = GameObject.Find("Finish");
        //if (fin == null)
        //{
        //    Debug.Log("Failded to find an object named 'Finish'");
        //    this.enabled = false;
        //    return;
        //}

        //finishScript = fin.GetComponent<Finish>();
        //if (finishScript == null)
        //{
        //    Debug.Log("finishScript is null");
        //    return;
        //}

        //Debug.Log("finishScript: " + finishScript);

        ////To see GetCherriesTillPrevious in Finish script
        //Debug.Log("Finish.GetCherriesTillPreviousScene(): "+ Finish.GetCherriesTillPreviousScene());
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries ++;
            //Changed from just cherries in only a current scene, to total number of cherries with cherries from previous scenes
            //cherriesText.text = "Cherries: " + cherries + Finish.GetCherriesTillPreviousScene();
            currentCherries = cherries + Finish.GetCherriesTillPreviousScene();
            //cherriesText.text = "Cherries: " + cherries;
            cherriesText.text = "Cherries: " + currentCherries;

        }
    }

    //public int GetCherries()
    //{
      //  return cherries;
    //}
}
