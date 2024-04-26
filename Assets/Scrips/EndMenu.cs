using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private AudioSource endSound;

    private void Start()
    {
        endSound = GetComponent<AudioSource>();
    }

    public void EndGame()
    {
        endSound.Play();
        Invoke("GoBackToStartScene", 1f);
    }

    private void GoBackToStartScene()
    {
        SceneManager.LoadScene("Start screen");
    }

}
