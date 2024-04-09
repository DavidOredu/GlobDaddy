using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public int maxHealth = 50;
    
   
    

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultuplier = 0.5f;

    [Header("Land State")]
    public bool spawnDust;

    [Header("Attack State")]
    public float comboTime = 0.2f;
    public float attackRadius = 0.5f;
    public float attack2Radius = 0.5f;
    public int attackDamage = 10;
    public int attack2Damage = 12;
    public int stunDamageAmount = 5;
    public LayerMask whatIsDamageable;

    [Header("Knockback Variables")]
    public bool knockback;
    public Vector2 knockbackVelocity;
    public float knockbackDuration;

    
    


    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float feetCheckRadius = 0.2f;
    public LayerMask whatIsGround;
}
