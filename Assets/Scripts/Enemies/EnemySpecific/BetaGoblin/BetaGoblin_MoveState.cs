using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaGoblin_MoveState : MoveState
{
    private BetaGoblin betaGoblin;
    public BetaGoblin_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, BetaGoblin betaGoblin) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.betaGoblin = betaGoblin;
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(betaGoblin.playerDetectedState);
        }

        else if(isDetectingWall || !isDetectingLedge)
        {
            betaGoblin.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(betaGoblin.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
