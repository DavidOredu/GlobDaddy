using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMeleeAttack2State : MeleeAttackState
{
    private BetaGoblin betaGoblin;
    
    public GoblinMeleeAttack2State(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, BetaGoblin betaGoblin) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(betaGoblin.meleeAttackState);
            }
            if (!isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(betaGoblin.lookForPlayerState);
            }
            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }

    public override void triggerAttack()
    {
        base.triggerAttack();

        
    }
}
