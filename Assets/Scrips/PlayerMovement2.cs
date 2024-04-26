using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

//This script is for Player Movement using Input System.

public class PlayerMovement2 : MonoBehaviour
{
    ////copied from PlayerMovement.cs

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    private PlayerAttack playerAttack;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float moveJumpSpeed = 0.1f;
    [SerializeField] private float gScalgeFalling = 2.5f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpingSoundEffect;

    private bool jumpBool;
    private bool isGrounded;
    public bool deadBool;

    //Turn this bool true while showing special animation other than ide, running or jumping.
    public bool specialAnim = false;

    //Make this true while jumping or idle which is control this script for animation
    //and make this flase while setting other animation enabled like attacking.
    public bool animStateNormal = true;

    private float timerJump;
    private float jumpBufferCounter;
    private float jumpBufferTime = 0.2f;

    //private float attackingTimeCounter;
    //[SerializeField] private float attackingTime = 0.3f;
    //[SerializeField] private float attackingTimeHold = 0.15f;
    //private float attackingTimeExtend;
    //[SerializeField] private float attackingArmSpeed = 30f;

    //For animator controll
    private string currentState;

    //For animation state
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUNNING = "Player_running";
    const string PLAYER_JUMPING = "Player_jumping";
    const string PLAYER_FALLING = "Player_falling";
    const string PLAYER_FLOATING = "Player_floating";
    const string PLAYER_FALLING_ONE = "Player_falling1";
    const string PLAYER_ATTACK_FRONT_BODY = "Player_attackFront_body";

    //For animation state
    private float velocityFloating = 2.5f;
    private float velocityFalling = -3f;

    ////For attack
    //private GameObject handF;
    //private SpriteRenderer spriteHandF;
    //private GameObject armF;
    //private LineRenderer lrF;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        playerAttack = GetComponent<PlayerAttack>();

        ////For attack
        //handF = GameObject.Find("handF");
        //spriteHandF = handF.GetComponent<SpriteRenderer>();
        //spriteHandF.enabled = false;
        //armF = GameObject.Find("armF");
        //lrF = armF.GetComponent<LineRenderer>();
        //lrF.enabled = false;

        //////// for Input System//////
        playerInputAction.Player.Jump.performed += OnJump;
        playerInputAction.Player.Horizontal.performed += OnHorizontal;
        playerInputAction.Player.Horizontal.canceled += OnHorizontalCanceled;
        //playerInputAction.Player.Attack.performed += Attack;
        //////////

        jumpBool = false;
        timerJump = 0f;
        jumpBufferCounter = 0f;
        //attackingTimeCounter = 0f;
        deadBool = false;

        ////Set handF position to the same position of player gameobject
        //handF.transform.position = transform.position;
    }

    private bool IsGrounded()
    {
        //A bug that you cannot jump and the movement gets slippy from some point occures if the value is .01f
        //The bug is not reproduced when the value is in range .1f to .03f
        //Debug.Log("Physics2D.BoxCast: " + Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround));
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    //private void Update()
    //{
    //    //dirX = Input.GetAxisRaw("Horizontal");

    //    //Debug.Log("dirX: " + dirX);
    //    rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

    //    //This is for old input manager.
    //    //if (Input.GetButtonDown("Jump") && IsGrounded())
    //    //{
    //    //    jumpingSoundEffect.Play();
    //    //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //    //}

    //    UpdateAnimationState();

    //}

    private void Update()
    {
        //Debug.Log(timerJump);
        //isGrounded = IsGrounded();
        timerJump -= Time.deltaTime;
        jumpBufferCounter -= Time.deltaTime;

        //attackingTimeCounter -= Time.deltaTime;


        ////Make the handF sprite flip state same as the one of player
        //if (sprite.flipX)
        //{
        //    spriteHandF.flipX = true;
        //}
        //else
        //{
        //    spriteHandF.flipX = false;
        //}
    }



    //FixedUpdate for rigid body
    private void FixedUpdate()
    {


        isGrounded = IsGrounded();

        //Debug.Log("isGrounded: " + isGrounded);
        //Debug.Log("IsGrounded(): " + IsGrounded());


        //jump with buffering
        if (isGrounded && Time.timeScale != 0 && jumpBufferCounter > 0.1f)
        {
            jumpingSoundEffect.Play();

            //Jump  (Original one)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0f;
        }


        //Horizontal movement during grounded and during jump
        if (isGrounded)
        {

            ////When not attacking
            //if (attackingTimeCounter <= 0f)
            //{
            //    //Here is the same as the basic horizontal movement.
            //    rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            //}
            ////When attacking
            //else
            //{
            //    rb.velocity = new Vector2(0f, 0f);
            //}


            //Here is the same as the basic horizontal movement.
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        }
        else
        {
            //Add force if the abs of the current velocity of x is smaller than max of abs of moveSpeed (with dir=1)
            if ((rb.velocity.x < 1 * moveSpeed && -1 * moveSpeed < rb.velocity.x) || rb.velocity.x * dirX < 0)
            {
                rb.AddForce(new Vector2(dirX * moveJumpSpeed, 0), ForceMode2D.Impulse);
            }


            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }

        //Better jump
        if (!isGrounded && rb.velocity.y < 0)
        {
            rb.gravityScale = gScalgeFalling;
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (gScalgeFalling - 1) * Time.fixedDeltaTime;
        }
        else
        {
            rb.gravityScale = 1f;
        }



        //Not to update animation state when you died.
        if (!deadBool)
        {
            UpdateAnimationState();
        }


        //Debug.Log("velocity y: " + rb.velocity.y);


    }




    //private void UpdateAnimationState()
    //{
    //    MovementState state;

    //    if (dirX > 0f)
    //    {
    //        state = MovementState.running;
    //        sprite.flipX = false;
    //    }
    //    else if (dirX < 0f)
    //    {
    //        state = MovementState.running;
    //        sprite.flipX = true;
    //    }
    //    else
    //    {
    //        state = MovementState.idle;
    //    }

    //    if (rb.velocity.y > .1f)
    //    {
    //        state = MovementState.jumping;
    //    }
    //    else if (rb.velocity.y < -.1f)
    //    {
    //        state = MovementState.falling;
    //    }

    //    anim.SetInteger("state", (int)state);
    //}


    //New animator system without using transition
    private void UpdateAnimationState()
    {

        //Flip the sprite 
        if (rb.velocity.x > 0f) sprite.flipX = false;
        else if (rb.velocity.x < 0f) sprite.flipX = true;



        //When grounded
        if (isGrounded)
        {
            ////Flip the sprite while grounded
            //if (dirX > 0f)
            //{
            //    sprite.flipX = false;
            //    //spriteHandF.flipX = false;
            //}
            //else if (dirX < 0f)
            //{
            //    sprite.flipX = true;
            //    //spriteHandF.flipX = true;
            //}


            //if (playerAttack.attackingTimeCounter > 0f)
            //{
            //    ChangeAnimationState(PLAYER_ATTACK_FRONT_BODY);
            //}

            if (playerAttack.attackingNow)
            {
                ChangeAnimationState(PLAYER_ATTACK_FRONT_BODY);
            }
            else if (dirX > 0f)
            {
                ChangeAnimationState(PLAYER_RUNNING);
                //sprite.flipX = false;
            }
            else if (dirX < 0f)
            {
                ChangeAnimationState(PLAYER_RUNNING);
                //sprite.flipX = true;
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }


        }
        //When not grounded
        else
        {
            ////Flip the sprite while jumping
            //if (rb.velocity.x > 0f) sprite.flipX = false;
            //else if (rb.velocity.x < 0f) sprite.flipX = true;


            if (rb.velocity.y >= velocityFloating)
            {
                ChangeAnimationState(PLAYER_JUMPING);
            }
            else if (rb.velocity.y > .1f && rb.velocity.y < velocityFloating)
            {
                ChangeAnimationState(PLAYER_FLOATING);
            }
            else if (rb.velocity.y < -.1f && rb.velocity.y >= velocityFalling)
            {
                ChangeAnimationState(PLAYER_FALLING_ONE);
            }
            else if (rb.velocity.y < velocityFalling)
            {
                ChangeAnimationState(PLAYER_FALLING);
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



    //////////////////


    //////// for Input Sytem

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



    public void OnJump(InputAction.CallbackContext context)
    {

        if (!playerAttack.attackingNow)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        //jumpBufferCounter = jumpBufferTime;

        ////This enable the running status only when timescale is not 0
        //if (isGrounded && Time.timeScale != 0 && timerJump < 0f)
        //{
        //    //Debug.Log("Hello");
        //    //jumpingSoundEffect.Play();
        //    jumpBool = true;
        //    //rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //    timerJump = 0.5f;
        //}

    }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        //Debug.Log("Horizontal :" + context.ReadValue<float>());

        //This enable the running status only when timescale is not 0
        if (Time.timeScale != 0f && !playerAttack.attackingNow)
        {
            dirX = context.ReadValue<float>();

        }
    }

    public void OnHorizontalCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log("Horizontal :" + context.ReadValue<float>());
        //Debug.Log("OnHorizontalCanceled");
        dirX = context.ReadValue<float>();
    }

    //public void Attack(InputAction.CallbackContext context)
    //{
    //    attackingTimeCounter = attackingTime;
    //    spriteHandF.enabled = true;
    //    lrF.enabled = true;
    //}

    //private void AttackHandMove()
    //{
    //    attackingTimeExtend = (attackingTime - attackingTimeHold) / 2;

    //    //If player is facing right
    //    if (!sprite.flipX)
    //    {
    //        //Arm is extending
    //        if (attackingTimeCounter > attackingTimeExtend + attackingTimeHold)
    //        {
    //            handF.transform.position = handF.transform.position + new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
    //        }
    //        //Arm is going back
    //        else if (attackingTimeCounter < attackingTimeExtend && attackingTimeCounter > 0f)
    //        {
    //            handF.transform.position = handF.transform.position - new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
    //        }
    //        //Arm is hold
    //        else if (attackingTimeCounter >= attackingTimeExtend & attackingTimeCounter <= attackingTimeExtend + attackingTimeHold)
    //        {
    //            handF.transform.position = handF.transform.position;
    //        }

    //    }
    //    //If player is facing left
    //    else
    //    {

    //        //Arm is extending
    //        if (attackingTimeCounter > attackingTimeExtend + attackingTimeHold)
    //        {
    //            handF.transform.position = handF.transform.position - new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
    //        }
    //        //Arm is going back
    //        else if (attackingTimeCounter < attackingTimeExtend && attackingTimeCounter > 0f)
    //        {
    //            handF.transform.position = handF.transform.position + new Vector3(attackingArmSpeed * Time.fixedDeltaTime, 0, 0);
    //        }
    //        //Arm is hold
    //        else if (attackingTimeCounter >= attackingTimeExtend & attackingTimeCounter <= attackingTimeExtend + attackingTimeHold)
    //        {
    //            handF.transform.position = handF.transform.position;
    //        }



    //    }
    //}



}


