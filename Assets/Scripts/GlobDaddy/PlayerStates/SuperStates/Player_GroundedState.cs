using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundedState : PlayerState
{
   
    protected bool isGrounded;
    protected bool isFeetTouchingGround;
    protected int XInput;
    protected bool jumpInput;
    protected bool attackInput;

    
    public Player_GroundedState(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player) : base(stateMachine, animBoolName, playerData, player)
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
        isFeetTouchingGround = player.CheckFeet();
    }

    public override void Enter()
    {
        base.Enter();
        
        player.jumpState.ResetAmountOfJumpLeft();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

       
        if (jumpInput && player.jumpState.CanJump())
        {
            
            player.inputHandler.UseJumpInput();
            
            stateMachine.ChangeState(player.jumpState);
        }
        else if (attackInput && XInput == 0 && player.weaponHandler.hasWeapon)
        {
            player.inputHandler.UseAttackInput();
            
            // get the animation using the attack index
            player.meleeAttackState.ResetAnimBool(player.weaponHandler.currentWeapon.attackCombos[player.meleeAttackState.currentAttackIndex]);
            stateMachine.ChangeState(player.meleeAttackState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }

        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.landState.SpawnDust();
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        XInput = player.inputHandler.NormalizedInputX;
        jumpInput = player.inputHandler.JumpInput;
        attackInput = player.inputHandler.AttackInput;
    }

}
