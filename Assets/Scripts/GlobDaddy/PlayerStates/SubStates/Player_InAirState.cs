using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InAirState : PlayerState
{
    
    protected bool isGrounded;
    protected int XInput;
    protected bool jumpInputStop;
    protected bool jumpInput;
    protected bool coyoteTime;
    protected bool isJumping;

    public Player_InAirState(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player) : base(stateMachine, animBoolName, playerData, player)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
      
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
        

        


        


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        XInput = player.inputHandler.NormalizedInputX;
        jumpInput = player.inputHandler.JumpInput;
        jumpInputStop = player.inputHandler.JumpInputStop;


        if (!isGrounded)
        {
            player.CheckIfShouldFlip(XInput);
            player.SetVelocityX(playerData.movementVelocity * XInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    public void CheckCoyoteTIme()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.jumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true;
    }

    public void SetIsJumping()
    {
        isJumping = true;
    }


    public override void LateUpdate()
    {
        base.LateUpdate();
        CheckCoyoteTIme();
        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if (jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        
    }
    public void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultuplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
}
