using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_PlayerDetectedState : PlayerDetectedState
{
    private Goblin goblin;
    public Goblin_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Goblin goblin) : base(etity, stateMachine, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(goblin.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            goblin.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(goblin.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(goblin.lookForPlayerState);
        }

        else if (!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(goblin.moveState);
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
