using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatDummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private bool applyKnockback;

    private float currentHealth, knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private GlobDaddyControllerBardent pc;
    private Animator anim;

    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<GlobDaddyControllerBardent>();

    }
    private void Damage(float[] details)
    {
        currentHealth -= details[0];
        
        

        if(playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        anim.SetBool("playerOnLeft", playerOnLeft);
        anim.SetTrigger("Damage");

        if(applyKnockback && currentHealth > 0.0f)
        {

        }
        if(currentHealth <= 0.0f)
        {

        }
    }
    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
    }
}
