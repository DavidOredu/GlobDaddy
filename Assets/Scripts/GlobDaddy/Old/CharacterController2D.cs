using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;
	// A position marking where to check if the player is grounded.
	[SerializeField] private Transform WallCheck;
	// A position marking where to check is the player is touching wall.
	[SerializeField] private Transform m_CeilingCheck;	
	// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    private int extraJumps;
    public int extraJumpValue;
	private bool isTouchingWall;
	public float wallCheckDistance;
	public bool isWallSliding;
	public float wallSlideSpeed;
	public float movementForceInAir;
	public float airDragMultiplier = 0.95f;
	public float variableJumpHeightMultiplier= 0.5f;
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
         }      
	
	void Start()
        {
            
	        extraJumps = extraJumpValue;
	    
	}
        void Update()
        {
		CheckIfWallSliding();
        }
	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		isTouchingWall = Physics2D.Raycast(WallCheck.position, transform.right, wallCheckDistance, m_WhatIsGround);
		newJump();
	}
	private void CheckIfWallSliding()
    {
		if (isTouchingWall && !m_Grounded && m_Rigidbody2D.velocity.y < 0)
        {
			isWallSliding = true;
        }
        else
        {
			isWallSliding = false;
        }
    }

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}
			if (isWallSliding)
			{
				if (m_Rigidbody2D.velocity.y < -wallSlideSpeed)
				{
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -wallSlideSpeed);
				}
			}
			if (m_Grounded)
			{
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
			else if (!m_Grounded && !isWallSliding && move != 0)
			{
				Vector2 forceToAdd = new Vector2(movementForceInAir * move, 0);
				m_Rigidbody2D.AddForce(forceToAdd);
				if (Mathf.Abs(m_Rigidbody2D.velocity.x) > 10f)
				{
					m_Rigidbody2D.velocity = new Vector2(10f * move, m_Rigidbody2D.velocity.y);
				}
			}
			else if (!m_Grounded && !isWallSliding && move == 0)
            {
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x * airDragMultiplier, m_Rigidbody2D.velocity.y);
            }
			// Move the character by finding the target velocity
			
			// And then smoothing it out and applying it to the character
			

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		
	}
	

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		transform.Rotate (0f, 180f, 0f);
	}
	public void newJump()
	{
		if (m_Grounded == true)
		{
			extraJumps = extraJumpValue;
		}
		if (Input.GetKeyDown(KeyCode.Space) && m_Grounded == true)
		{
			// Add a vertical force to the player.

			m_Rigidbody2D.velocity = Vector2.up * m_JumpForce;		
		}
		
	}	
    private void OnDrawGizmos()
    {
		Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);

		Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + wallCheckDistance, WallCheck.position.y, WallCheck.position.z));
    }


}

