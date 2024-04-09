using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobDaddyCombatController : MonoBehaviour
{
    [SerializeField]
    private int attack1Damage, stunDamageAmount;
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1radius ;
 
    
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private bool gotInput, isAttacking, isFirstAttack, isHoldingWeapon;

    private float lastInputTime = Mathf.NegativeInfinity;
    private WeaponHandler weaponHandler;

    private AttackDetails attackDetails;
    private Animator anim;

    public Animator camAnim;

    private GlobDaddyControllerBardent PC;

    private GlobDaddyStats GlobDaddyStats;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        GlobDaddyStats = GetComponent<GlobDaddyStats>();
        weaponHandler = GetComponent<WeaponHandler>();
        PC = GetComponent<GlobDaddyControllerBardent>();
    }


    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
        CheckIfHoldingWeapon();
        
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(combatEnabled && weaponHandler.hasWeapon)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }
    private void CheckAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
                camAnim.SetBool("Shake", true);
            }
        }
        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;


        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);

        }
    }
    private void CheckIfHoldingWeapon()
    {
        if(weaponHandler.hasWeapon)
        {
            isHoldingWeapon = true;
        }
        else
        {
            isHoldingWeapon = false;
        }
    }
    private void FinishAttack1()
    {
       isAttacking = false;
       anim.SetBool("isAttacking", isAttacking);
       anim.SetBool("attack1", false);
        camAnim.SetBool("Shake", false);
    }
    private void Damage(AttackDetails attackDetails)
    {
        int direction;

        GlobDaddyStats.DecreaseHealth(attackDetails.damageAmount);

        if(attackDetails.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        PC.Knockback(direction);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1radius);
    }
}
