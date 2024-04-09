using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaGoblin_IdleState : IdleState
{
    private BetaGoblin betaGoblin;
    public BetaGoblin_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, BetaGoblin betaGoblin) : base(etity, stateMachine, animBoolName, stateData)
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

        else if (isIdleTImeOver)
        {
            stateMachine.ChangeState(betaGoblin.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
