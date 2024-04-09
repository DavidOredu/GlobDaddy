using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player) : base(stateMachine, animBoolName, playerData, player)
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
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        player.SetVelocityX(playerData.movementVelocity * XInput);

        
        if (XInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (!isGrounded)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.CheckIfShouldFlip(XInput);
        
    }
}
