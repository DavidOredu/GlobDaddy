using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MeleeAttackState : Player_AbilityState
{


    public Player_MeleeAttackState(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player) : base(stateMachine, animBoolName, playerData, player)
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
        currentAttackIndex++;
        firstAttack = true;
        player.SetVelocityX(0f);
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
            if (attackInput && !isComboTimeOver)
            {
                player.inputHandler.UseAttackInput();
                CheckWeaponCombos();
                player.meleeAttackState.ResetAnimBool(player.weaponHandler.currentWeapon.attackCombos[player.meleeAttackState.currentAttackIndex]);
                stateMachine.ChangeState(player.meleeAttackState);
            }
            
            else if( XInput != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else if ( XInput == 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckIfComboTimeIsOver();
        RunComboTimeEnd();
    }
    
    public void RunComboTimeEnd()
    {
        if (isComboTimeOver)
            currentAttackIndex = 0;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool CanAttack()
    {
        if (firstAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
