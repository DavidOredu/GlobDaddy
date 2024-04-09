using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaGoblin : Entity
{
    public BetaGoblin_MoveState moveState { get; private set; }
    public BetaGoblin_IdleState idleState { get; private set; }
    public BetaGoblin_PlayerDetectedState playerDetectedState { get; private set; }
    public BetaGoblin_MeleeAttackState meleeAttackState { get; private set; }
    public GoblinMeleeAttack2State meleeAttack2State { get; private set; }
    public BetaGoblin_RangeAttackState rangeAttackState { get; private set; }
    public BetaGoblin_LookForPlayerState lookForPlayerState { get; private set; }
    public BetaGoblin_StunState stunState { get; private set; }
    public BetaGoblin_DeadState deadState { get; private set; }
    public HealthBar healthBar;



    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttack2StateData;
    [SerializeField]
    private D_RangeAttackState rangeAttackStateData;
    [SerializeField]
    private D_StunState stunStateStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform meleeAttack2Position;
    [SerializeField]
    private Transform rangeAttackPosition;


    public override void Start()
    {
        base.Start();

        moveState = new BetaGoblin_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new BetaGoblin_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new BetaGoblin_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        meleeAttackState = new BetaGoblin_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        meleeAttack2State = new GoblinMeleeAttack2State(this, stateMachine, "meleeAttack2", meleeAttack2Position, meleeAttack2StateData, this);
        rangeAttackState = new BetaGoblin_RangeAttackState(this, stateMachine, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);
        lookForPlayerState = new BetaGoblin_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new BetaGoblin_StunState(this, stateMachine, "stun", stunStateStateData, this);
        deadState = new BetaGoblin_DeadState(this, stateMachine, "dead", deadStateData, this);


        stateMachine.Initialize(moveState);
        healthBar.SetMaxHealth(entityData.maxHealth);
        healthBar.gameObject.SetActive(false);

    }

    public override void Damage(AttackDetails attackDetails)
    {
        healthBar.gameObject.SetActive(true);
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }

        else if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangeAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        Gizmos.DrawWireSphere(meleeAttack2Position.position, meleeAttack2StateData.attackRadius);
    }

    public override void Update()
    {
        base.Update();

        healthBar.SetHealth(currentHealth);
        healthBar.SetHealthToZero(currentHealth);
    }
}
