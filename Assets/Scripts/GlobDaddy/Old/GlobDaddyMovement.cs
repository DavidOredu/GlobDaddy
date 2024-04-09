using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobDaddyMovement : MonoBehaviour
{
   
    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Rigidbody2D rb;
    
   

    

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
	animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("IsWallSliding", controller.isWallSliding);

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
	    animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        if (Input.GetButtonUp("Jump") && controller.m_Grounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * controller.variableJumpHeightMultiplier);
        }

    }


    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        controller.newJump();
    }

}
