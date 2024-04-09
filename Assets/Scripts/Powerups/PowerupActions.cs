using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerupActions : MonoBehaviour
{
    public ParticleSystem ShieldParticle;
    [SerializeField]
    private PowerupBehaviour powerupBehaviour;



    private GameObject player;
    private Player playerScript;

    public float shieldStartLifetime = 7f;
    public static bool isShieldOn = false;
    public float ShieldParticleDuration = 7f;
    [SerializeField]
    private PlayerData playerData;

   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        var main = ShieldParticle.main;
        main.duration = ShieldParticleDuration;
        main.startLifetime = shieldStartLifetime;
    }
    #region Shield Powerup
    public void ShieldStartAction()
    {
        isShieldOn = true;
        
        
        Instantiate(ShieldParticle, player.transform);
        
       
        

    }

    public void ShieldEndAction()
    {
        isShieldOn = false;
    }
    #endregion

    #region Health Powerup
    public void HealthStartAction()
    {
        playerScript.currentHealth += 20;
    }

    public void HealthEndAction()
    {

    }
    #endregion

    #region Coins
    public void CoinStartAction()
    {

    }

    public void CoinEndAction()
    {

    }
    #endregion
}
