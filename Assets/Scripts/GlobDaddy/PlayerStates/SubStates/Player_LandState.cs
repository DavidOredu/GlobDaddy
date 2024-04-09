using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LandState : Player_GroundedState
{
    public Player_LandState(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player) : base(stateMachine, animBoolName, playerData, player)
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
        playerData.spawnDust = true;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        if (XInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        SpawnDust();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }

    public void SpawnDust()
    {
        if (isGrounded == true)
        {
            if (playerData.spawnDust == true)
            {
                GameObject.Instantiate(player.dustPS, player.GroundCheck.position, Quaternion.identity);
                playerData.spawnDust = false;
            }
        }
        else
        {
            playerData.spawnDust = true;
        }

    }
}
