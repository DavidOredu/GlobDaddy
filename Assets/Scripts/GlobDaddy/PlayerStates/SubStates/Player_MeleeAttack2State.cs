using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MeleeAttack2State : Player_AbilityState
{
    public Player_MeleeAttack2State(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player) : base(stateMachine, animBoolName, playerData, player)
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
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckIfComboTimeIsOver();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
