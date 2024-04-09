using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AbilityState : PlayerState
{
    
    protected bool isAbilityDone;
    protected bool isComboTimeOver;
    protected bool isGrounded;
    protected bool attackInput;
    protected int XInput;
    protected bool firstAttack;

    public int currentAttackIndex = 0;

    public Player_AbilityState(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player) : base(stateMachine, animBoolName, playerData, player)
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

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        

        if (isAbilityDone)
        {
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.inAirState);
            }
        }
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckWeaponCombos();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        attackInput = player.inputHandler.AttackInput;
        XInput = player.inputHandler.NormalizedInputX;
    }
    public void CheckWeaponCombos()
    {
        if (!player.weaponHandler.hasWeapon) { return; }
        if (currentAttackIndex >= player.weaponHandler.currentWeapon.maxCombo)
            currentAttackIndex = 0;
    }
    public bool CheckIfComboTimeIsOver()
    {
        if(Time.time >= player.inputHandler.lastInputTime + playerData.comboTime)
        {
            return isComboTimeOver = true;
        }
        else
        {
            return isComboTimeOver = false;
        }
    }
    
}
