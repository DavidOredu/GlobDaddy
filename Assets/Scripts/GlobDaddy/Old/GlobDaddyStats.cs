using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobDaddyStats : MonoBehaviour
{
    [SerializeField]
    private float
        maxHealth;
    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    private float currentHealth;

    private Animator anim;

    private GameManager GM;

    private void Start()
    {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    public void DecreaseHealth(float amount)
    {
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            anim.SetTrigger("Dead");
            Die();
        }

        
    }
    private void Die()
    {
        
        
        GM.Respawn();
        Destroy(gameObject);
    }
}
