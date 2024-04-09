using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_MoveState : MoveState
{
    private Goblin goblin;

    public Goblin_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Goblin goblin) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(goblin.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            goblin.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(goblin.idleState);
        }
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
