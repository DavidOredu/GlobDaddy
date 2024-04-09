using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaGoblin_LookForPlayerState : LookForPlayerState
{
    private BetaGoblin betaGoblin;
    public BetaGoblin_LookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, BetaGoblin betaGoblin) : base(etity, stateMachine, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(betaGoblin.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
