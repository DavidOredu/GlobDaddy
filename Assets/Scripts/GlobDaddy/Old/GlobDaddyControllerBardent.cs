using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobDaddyControllerBardent : MonoBehaviour
{
    private float movementInputDirection;
    private float turnTimer;
    public float movementSpeed = 10f;
    public float jumpForce = 16;
    private bool isFacingRight = true;
    private Animator anim;
    private bool isWalking;
    public Transform groundCheck;
    public Transform wallCheck;
    //public Transform ledgeCheck;
    private bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool canJump;
    public int amountOfJumps = 1;
    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private bool isTouchingWall;
    public float wallCheckDistance;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration;

    public float turnTimerSet = 0.1f;
    private bool isWallSliding ;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    private float jumpTimer;
    public float jumpTimerSet = 0.15f;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool spawnDust;
    public GameObject dustPS;
    //private bool isTouchingLedge;
    //private bool canClimbLedge = false;
    //private bool ledgeDetected;
    private bool canMove;
    private bool canFlip;
    private bool knockback;
    //private Vector2 ledgePosBot;
    //private Vector2 ledgePos1;
    //private Vector2 ledgePos2;
    //public float ledgeClimbXOffset1 = 0f;
    //public float ledgeClimbYOffset1 = 0f;
    //public float ledgeClimbXOffset2 = 0f;
    //public float ledgeClimbYOffset2 = 0f;

    [SerializeField]
    private Vector2 knockbackSpeed;
    



    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            if (spawnDust == true)
            {
                Instantiate(dustPS, groundCheck.position, Quaternion.identity);
                spawnDust = false;
            }
        }
        else
        {
            spawnDust = true;
        }
        checkInput();
        checkMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        checkIfWallSliding();
        CheckJump();
        CheckKnockback();
        //CheckLedgeClimb();
    }
    private void FixedUpdate()
    {
        applyMovement();
        CheckSurroundings();
    }
    private void checkMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    public int GetFacingDirection()
    {
        return facingDirection;
    }
    private void checkInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0))
            {
                Jump();
               

            }
            
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if(!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }
        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if(turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }
        

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }
    private void checkIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0 && movementInputDirection == facingDirection)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void UpdateAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(movementInputDirection));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("YVelocity", rb.velocity.y);
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        //isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        //if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        //{
           // ledgeDetected = true;
          //  ledgePosBot = wallCheck.position;
        //}
    }
    private void applyMovement()
    {
        if (!isGrounded && !isWallSliding && movementInputDirection == 0 && !knockback)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !knockback)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y); 
        }
        
        

        if (isWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }
    //private void CheckLedgeClimb()
    //{
     //   if (ledgeDetected && !canClimbLedge)
       // {
          //  canClimbLedge = true;

          //  if (isFacingRight)
            //{
            //    ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
             //   ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
          //  }
         //   else
          //  {
           //     ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
          //      ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            //}

          //  canMove = false;
          //  canFlip = false;

           // anim.SetBool("canClimbLedge", canClimbLedge);
       // }

      //  if (canClimbLedge)
      //  {
          //  transform.position = ledgePos1;
       // }
  //  }
    
   //public void FinishLegdeClimb()
   //{
     //   canClimbLedge = false;
      //  transform.position = ledgePos2;
      //  canMove = true;
      //  canFlip = true;
      //  ledgeDetected = false;
       // anim.SetBool("canClimbLedge", canClimbLedge);
   // }
   public void DisableFlip()
    {
        canFlip = false;
    }
    public void EnableFlip()
    {
        canFlip = true;
    }
    private void Flip()
    {
        if (canFlip && !isWallSliding && !knockback)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
        
    }
    private void Jump()
    {
        if (canJump)
        {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
           

        }
    }
    private void CheckJump()
    {
        if (jumpTimer > 0)
        {
            if (isGrounded)
            {
                Jump();
                
            }
        }
        
        if (isAttemptingToJump) 
        {
            jumpTimer -= Time.deltaTime;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.05f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
}
