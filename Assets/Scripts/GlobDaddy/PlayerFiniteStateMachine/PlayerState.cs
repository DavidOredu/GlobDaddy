using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    
    protected PlayerStateMachine stateMachine;
    
    protected PlayerEntity playerEntity;
    protected PlayerData playerData;
    protected Player player;

    protected bool isAnimationFinished;

    protected float startTime { get; private set; }

    private string animBoolName;

    public PlayerState(PlayerStateMachine stateMachine, string animBoolName, PlayerData playerData, Player player)
    {
        
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.player = player;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
        isAnimationFinished = false;
    }
    public virtual void ResetAnimBool(string newAnimBoolName)
    {
        this.animBoolName = newAnimBoolName;
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void LateUpdate()
    {

    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }
}
