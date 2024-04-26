using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //private Rigidbody2D rb;
    private GameObject player;
    private GameObject mainCamera;
    private Camera cam;
    private BoxCollider2D boxColl;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private int maxHealth = 1;
    private int currentHealth;
    private bool dead = false;
    private Vector3 playerPosition;
    private Vector3 enemyPosition;
    private Vector3 enemyToPlayer;
    private float speed = 50f;

    private Vector3 enemyScreenPos;
    private float edgeX = 0.1f;
    private float edgeY = 0.1f;
    private bool enemyDieFilp = false;
    private int enemyDieBounce = 0;
    private int enemyDieBounceMax = 3;
    private Vector3 velocity;

    private string currentState;

    const string ENEMY_DEAD = "Enemy_dead";



    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
        cam = mainCamera.GetComponent<Camera>();
        boxColl = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        //rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            DeadMotion();
        }
    }

    //Make it public so that you can call this method in PlayerAttack script
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died");
        dead = true;
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
        enemyToPlayer = (playerPosition - enemyPosition).normalized;

        //Disable BoxCollider2D component when enemy dies in order not to attack player after the enemy died
        boxColl.enabled = false;

        //Make the sorting layer of this object upper than terrain
        sprite.sortingLayerName = "DeadEnemy";

        ChangeAnimationState(ENEMY_DEAD);

    }

    private void DeadMotion()
    {
        //Enemey bounces three times (enemyDieBounce = 0, 1, 2)
        if (enemyDieBounce < enemyDieBounceMax)
        {
            if (enemyDieFilp)
            {
                transform.position = transform.position + enemyToPlayer * speed * Time.fixedDeltaTime;
            }
            else
            {
                transform.position = transform.position - enemyToPlayer * speed * Time.fixedDeltaTime;
            }
        }
        else if (enemyDieBounce == enemyDieBounceMax)
        {
            velocity += Physics.gravity * Time.fixedDeltaTime;

            if (enemyDieFilp)
            {
                transform.position = transform.position + (enemyToPlayer * speed * 0.2f + velocity) * Time.fixedDeltaTime;
            }
            else
            {
                transform.position = transform.position - (enemyToPlayer * speed * 0.2f + velocity) * Time.fixedDeltaTime;
            }
        }

        //transform.position = transform.position + enemyToPlayer * speed * Time.fixedDeltaTime;

        //Get position in Screen. left bottom is (0, 0)
        enemyScreenPos = cam.WorldToScreenPoint(transform.position);

        //If enemy is at the edge of the screen
        if (enemyDieBounce < enemyDieBounceMax)
        {
            if (enemyScreenPos.x < edgeX * cam.pixelWidth
    || enemyScreenPos.x > (1 - edgeX) * cam.pixelWidth
    || enemyScreenPos.y < edgeY * cam.pixelHeight
    || enemyScreenPos.y > (1 - edgeY) * cam.pixelHeight)
            {
                enemyDieFilp = !enemyDieFilp;
                enemyDieBounce += 1;
            }

        }
    }

    //To control animator nicer
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;

    }
}
