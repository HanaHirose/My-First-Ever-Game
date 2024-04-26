using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMovement2 playerMovement2;
    private PlayerAttack playerAttack;

    private Gamepad gamepad;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement2 = GetComponent<PlayerMovement2>();
        playerAttack = GetComponent<PlayerAttack>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Enemy") && !playerAttack.attackHit)
        {
           
            Debug.Log("player touched an enemy!");
            Die();
        }
    }
    

    private void Die()
    {
        playerMovement2.deadBool = true;

        //Set onceDeadBool in GameStatus script true if you die
        GameStatus.MakeTrueOnceDeadBool();

        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Rumble();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Rumble()
    {
        gamepad = Gamepad.current;

        if (gamepad != null)
        {
            Debug.Log("Start Rumble");
            gamepad.SetMotorSpeeds(1f, 1f);
            Invoke(nameof(StopRumble), 0.7f);
        }
        else
        {
            Debug.Log("gamepad is null");
        }
    }

    private void StopRumble()
    {
        Debug.Log("Stop rumble");
        gamepad.SetMotorSpeeds(0f, 0f);
    }

}
