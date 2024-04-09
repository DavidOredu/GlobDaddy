using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour : MonoBehaviour
{
    public PowerupController powerupController;
    
    [SerializeField]
    private Powerup powerup;

    private Transform transform_;

    
    private void Awake()
    {
        transform_ = transform;
    }
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            ActivatePowerup();
            gameObject.SetActive(false);
            
        }
    }

    private void ActivatePowerup()
    {
        powerupController.ActivatePowerup(powerup);
    }

    public void SetPowerup(Powerup powerup)
    {
        this.powerup = powerup;
        gameObject.name = powerup.name;
    }
}
