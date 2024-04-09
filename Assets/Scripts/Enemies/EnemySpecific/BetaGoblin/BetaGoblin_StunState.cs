using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaGoblin_StunState : StunState
{
    private BetaGoblin betaGoblin;
    public BetaGoblin_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, BetaGoblin betaGoblin) : base(etity, stateMachine, animBoolName, stateData)
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

        if (isStunTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(betaGoblin.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(betaGoblin.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
