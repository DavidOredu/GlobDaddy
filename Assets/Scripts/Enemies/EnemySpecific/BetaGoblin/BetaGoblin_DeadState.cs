using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaGoblin_DeadState : DeadState
{
    private BetaGoblin betaGoblin;
    public BetaGoblin_DeadState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, BetaGoblin betaGoblin) : base(etity, stateMachine, animBoolName, stateData)
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
        entity.SetVelocity(0f);
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
    }
}
