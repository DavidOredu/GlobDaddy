using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Variables

    [SerializeField]
    private PlayerData playerData;

    [HideInInspector]
    public int currentHealth;

    public float knockbackStartTime;

    
    private AttackDetails attackDetails;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public GameManager GM { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public WeaponHandler weaponHandler { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public SoundManager soundManager { get; private set; }
    public HealthBar healthBar { get; private set; }

    #endregion
    #region Check Variables
    public Transform attackPos;

    public GameObject bloodParticle;
    public GameObject dustPS;
    public Transform GroundCheck;
    public Transform feetCheck;
    public Transform feet2Check;


    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;
    protected bool isDead;

    public int FacingDirection { get; private set; }
    #endregion
    #region Input Variables
    

    // public int XInput;
    //public int UIXinput;
    //public Button UILeft;
    #endregion

    public PlayerStateMachine StateMachine { get; private set;  }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_InAirState inAirState { get; private set; }
    public Player_LandState landState { get; private set; }
    public Player_MeleeAttackState meleeAttackState {get; private set;}
    public Player_MeleeAttack2State meleeAttack2State { get; private set; }
    public Player_DeadState deadState { get; private set; }


    

    

    public void Awake()
    {

        StateMachine = new PlayerStateMachine();

        idleState = new Player_IdleState(StateMachine, "idle", playerData, this);
        moveState = new Player_MoveState(StateMachine, "move", playerData, this);
        jumpState = new Player_JumpState(StateMachine, "inAir", playerData, this);
        inAirState = new Player_InAirState(StateMachine, "inAir", playerData, this);
        landState = new Player_LandState( StateMachine, "land", playerData, this);
        meleeAttackState = new Player_MeleeAttackState(StateMachine, "attack1", playerData, this);
        meleeAttack2State = new Player_MeleeAttack2State(StateMachine, "attack2", playerData, this);
        deadState = new Player_DeadState(StateMachine, "dead", playerData, this);



    }

    public void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthBar = GameObject.Find("GlobDaddyHealthBar").GetComponent<HealthBar>();
        weaponHandler = GetComponent<WeaponHandler>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        inputHandler = GetComponent<PlayerInputHandler>();
        
        // UILeft = GameObject.Find("Left").GetComponent<Button>();
        StateMachine.Initialize(idleState);
        FacingDirection = 1;
        currentHealth = playerData.maxHealth;
        healthBar.SetMaxHealth(playerData.maxHealth);
    }

    

    public void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        healthBar.SetHealth(currentHealth);
        healthBar.SetHealthToZero(currentHealth);
        CurrentVelocity = RB.velocity;
        CheckKnockback();
        
    }

    public void LateUpdate()
    {
        StateMachine.CurrentState.LateUpdate();
        
        CurrentVelocity = RB.velocity;
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    public void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    // public void SetXInput(int Xinput)
    //{
    //  if(Xinput != 0)
    //{
    //   UIXinput = Xinput;
    // }
    //else if(Xinput == 0)
    //{
    //  UIXinput = 0;
    //}


    //}
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int XInput)
    {
        if (XInput != 0 && XInput != FacingDirection)
        {
            Flip();
        }

    }

    public void CheckIfMoveLeft(int XInput)
    {
        if (XInput < 0)
        {
            SetVelocityX(playerData.movementVelocity * -1);
        }
        if (XInput != 0 && XInput != FacingDirection && CurrentVelocity.x <= 2f)
        {
            Flip();
        }
    }






    public void CheckIfMoveRight(int XInput)
    {
        if (XInput > 0)
        {
            SetVelocityX(playerData.movementVelocity * 1);
        }
    }

    public bool CheckFeet()
    {
        return Physics2D.OverlapCircle(feetCheck.position, playerData.feetCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    #endregion

    #region Other Functions
    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPos.position, playerData.attackRadius, playerData.whatIsDamageable);

        attackDetails.damageAmount = playerData.attackDamage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = playerData.stunDamageAmount;


        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);

        }  
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        int direction;
        if (!PowerupActions.isShieldOn)
        {
            DecreaseHealth(attackDetails.damageAmount);


            if (attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            if (currentHealth <= 0.0f)
            {
                isDead = true;
            }

            if (isDead)
            {
                StateMachine.ChangeState(deadState);
            }


            Knockback(direction);
        }

       
    }

    public virtual void DecreaseHealth(int amount)
    {
        Instantiate(bloodParticle, transform.position, bloodParticle.transform.rotation);
        currentHealth -= amount;

  

    }

    public void Knockback(int direction)
    {
        playerData.knockback = true;
        knockbackStartTime = Time.time;
        RB.velocity = new Vector2(playerData.knockbackVelocity.x * direction, playerData.knockbackVelocity.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + playerData.knockbackDuration && playerData.knockback)
        {
            playerData.knockback = false;
            RB.velocity = new Vector2(0.0f, RB.velocity.y);
        }
    }

    public void Die()
    {


        GM.Respawn();
        Destroy(gameObject);
    }



    #endregion

    #region PlaySound Functions
    private void PlayFootstepSound()
    {
        soundManager.PlaySound("Walk");

    }

    private void PlayJumpScoff()
    {
        soundManager.PlaySound("JumpScoff");
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, playerData.groundCheckRadius);
        Gizmos.DrawWireSphere(feetCheck.position, playerData.feetCheckRadius);
        Gizmos.DrawWireSphere(feet2Check.position, playerData.feetCheckRadius);
        Gizmos.DrawWireSphere(attackPos.position, playerData.attackRadius);

    }

}
