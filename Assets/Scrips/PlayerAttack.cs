using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Move codes about attacking from PlayerMovement2.cs to this script PlayerAttack.cs

public class PlayerAttack : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    public float attackingTimeCounter;
    [SerializeField] private float attackingTime = 0.3f;
    [SerializeField] private float attackingTimeHold = 0.15f;
    private float attackingTimeExtend;
    [SerializeField] private float attackingArmSpeed = 30f;

    //For animator controll
    private string currentState;

    //For animator controll
    //const string PLAYER_ATTACK_FRONT_BODY = "Player_attackFront_body";

    //For attack
    private GameObject handF;
    private SpriteRenderer spriteHandF;
    private GameObject armF;
    private LineRenderer lrF;

    //private GameObject player;
    //private PlayerMovement2 pm2;

    private bool currentHoldPoint = false;
    private float speedHold = 10f;
    private Vector3 playerToEnemy;
    private Vector3 rotatedPlayerToEnemy;
    private Vector3 moveHoldTo;
    private float speedMoveToEnemy = 30f;
    private float attackRangePlayer = 0.5f;
    private float amplitude = 0.3f;


    //attackHit is true from when player starts to grab an enemy till when player moved and touched to the enemy
    public bool attackHit = false;
    private Collider2D grabbedEnemy;

    //attackingNow is true when player is attacking including when not hitting enemy.
    public bool attackingNow = false;

    private bool attackCanceled = false;



    private void Start()
    {

        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //For attack
        handF = GameObject.Find("handF");
        spriteHandF = handF.GetComponent<SpriteRenderer>();
        spriteHandF.enabled = false;
        armF = GameObject.Find("armF");
        lrF = armF.GetComponent<LineRenderer>();
        lrF.enabled = false;

        //To use specialAnim bool in PlayerMovement2.cs
        //player = GameObject.Find("Player");
        //pm2 = player.GetComponent<PlayerMovement2>();


        //For input system
        playerInputAction.Player.Attack.performed += Attack;
        playerInputAction.Player.Attack.canceled += AttackCanceled;

        attackingTimeCounter = 0f;

        //Set handF position to the same position of player gameobject
        handF.transform.position = transform.position;

        attackCanceled = false;

    }

    private void Update()
    {
        attackingTimeCounter -= Time.deltaTime;


        //Make the handF sprite flip state same as the one of player
        if (sprite.flipX)
        {
            spriteHandF.flipX = true;
        }
        else
        {
            spriteHandF.flipX = false;
        }


        //Call Attack function only when player attacks. See if there is collider or not
        if (attackingTimeCounter > 0f)
        {
            AttackCollider();
        }

        // if player is grabbing an enemy
        if (attackHit && grabbedEnemy != null)
        {
            AttackHold(grabbedEnemy);
            if (attackCanceled)
            {
                MoveToEnemey(grabbedEnemy);
            }

        }

        //When attacking is completed
        if(attackingTimeCounter <= 0f && !attackHit)
        {
            attackingNow = false;
        }
    }




    private void FixedUpdate()
    {

        //if (comeClose is true)
        //  move playe body to hand 
        //else if( hold is true)
        //  hold the hand
        //else
        //  AttackHandMove();


        //Only before grabbing an emeny if player grabs an enemy,
        //or from the beginning of attacking till the end of attacking if player doesn't grab any enemy,
        //Move hand 
        if (!attackHit)
        {
            //Extend hand for attacking
            AttackHandMove();
        }


        //Call this once when finishing attacking
        //Hide sprite of Hand after finishing attacking
        if (attackingTimeCounter <= 0f && !attackHit)
        {
            if (spriteHandF.enabled)
            {
                spriteHandF.enabled = false;
                lrF.enabled = false;

                //Set handF position to the same position of player gameobject
                handF.transform.position = transform.position;

            }
        }


    }





    private void AttackCollider()
    {
        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);

        //Debug.Log("collider: " + Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer));
        if (enemy != null)
        {
            //hold = true;
            ////turn hold to be false when cancel the attack key of when the attack key is already canceled
            ////trun comeClose true when cancel the attack key
            ///

            //AttackHold(enemy);

            grabbedEnemy = enemy;


            attackHit = true;
            Debug.Log("We hit " + enemy.name);

            //Call TakeDamage method in Enemy script.
            //Enemy script is attached to Enemy1 gameobject
            //enemy.GetComponent<Enemy>().TakeDamage(1);


        }
        //if(grabbedEnemy != null)
        //{
        //    MoveToEnemey(grabbedEnemy);
        //}

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    //Hold enemy with vibration of hand
    private void AttackHold(Collider2D enemy)
    {
        playerToEnemy = (transform.position - enemy.transform.position).normalized;
        rotatedPlayerToEnemy = Vector3FromAngle(-90);

        if (!currentHoldPoint)
        {
            handF.transform.position = Vector2.MoveTowards(handF.transform.position, enemy.transform.position, Time.deltaTime * speedHold);

            if (Vector2.Distance(handF.transform.position, enemy.transform.position) < .1f)
            {
                currentHoldPoint = !currentHoldPoint;
            }
        }
        else
        {
            moveHoldTo = enemy.transform.position + rotatedPlayerToEnemy * amplitude;
            handF.transform.position = Vector2.MoveTowards(handF.transform.position, moveHoldTo, Time.deltaTime * speedHold);

            if (Vector2.Distance(handF.transform.position, moveHoldTo) < .1f)
            {
                currentHoldPoint = !currentHoldPoint;
            }
        }

    }

    private void MoveToEnemey(Collider2D enemy)
    {
        //NEED TO IMPROVE
        //It is better to use different layer from EnemyLayer.
        Collider2D collEnemy = Physics2D.OverlapCircle(transform.position, attackRangePlayer, enemyLayer);

        if (collEnemy == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, Time.deltaTime * speedMoveToEnemy);
        }
        else
        {
            enemy.GetComponent<Enemy>().TakeDamage(1);
            attackHit = false;
        }
    }


    public void Attack(InputAction.CallbackContext context)
    {
        attackingTimeCounter = attackingTime;
        spriteHandF.enabled = true;
        lrF.enabled = true;
        attackingNow = true;
        attackCanceled = false;

    }

    public void AttackCanceled(InputAction.CallbackContext context)
    {
        attackCanceled = true;
    }

    private void AttackHandMove()
    {
        attackingTimeExtend = (attackingTime - attackingTimeHold) / 2;

        //If player is facing right
        if (!sprite.flipX)
        {
            //Arm is extending
            if (attackingTimeCounter > attackingTimeExtend + attackingTimeHold)
            {
                handF.transform.position = handF.transform.position + new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
            }
            //Arm is going back
            else if (attackingTimeCounter < attackingTimeExtend && attackingTimeCounter > 0f)
            {
                handF.transform.position = handF.transform.position - new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
            }
            //Arm is hold
            else if (attackingTimeCounter >= attackingTimeExtend && attackingTimeCounter <= attackingTimeExtend + attackingTimeHold)
            {
                handF.transform.position = handF.transform.position;
            }

        }
        //If player is facing left
        else
        {

            //Arm is extending
            if (attackingTimeCounter > attackingTimeExtend + attackingTimeHold)
            {
                handF.transform.position = handF.transform.position - new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
            }
            //Arm is going back
            else if (attackingTimeCounter < attackingTimeExtend && attackingTimeCounter > 0f)
            {
                handF.transform.position = handF.transform.position + new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
            }
            //Arm is hold
            else if (attackingTimeCounter >= attackingTimeExtend && attackingTimeCounter <= attackingTimeExtend + attackingTimeHold)
            {
                handF.transform.position = handF.transform.position;
            }



        }

    }

    private Vector3 Vector3FromAngle(float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 1f);
    }

    //private void UpdateAnimationState()
    //{
    //}


    //To control animator nicer-------
    //private void ChangeAnimationState(string newState)
    //{
    //    if (currentState == newState) return;

    //    anim.Play(newState);

    //    currentState = newState;

    //}
    //------------


    //For input system---------
    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();

    }

    private void OnEnable()
    {
        playerInputAction.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Player.Disable();
    }
    //---------

}