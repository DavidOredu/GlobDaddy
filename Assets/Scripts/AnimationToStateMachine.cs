using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    private AttackDetails attackDetails;
    public D_Entity entityData;

    
    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }

    public AttackState attackState;
    private void TriggerAttack()
    {
        attackState.triggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
    public void PlayerKnockback()
    {
        int direction;
        if (attackDetails.position.x < player.transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        rb.velocity = new Vector2(entityData.playerKnockbackSpeed * direction, rb.velocity.y);
    }
}
