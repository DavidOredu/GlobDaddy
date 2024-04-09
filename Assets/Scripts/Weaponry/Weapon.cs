using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    // Start is called before the first frame update
    private WeaponHandler weaponHandler;
    public WeaponData weaponData;
    public Transform attackPos;
    public Animator lightAnim;

    public int maxCombo;

    public string[] attackCombos;
    private void Start()
    {
        attackCombos = weaponData.attackCombos;
        maxCombo = attackCombos.Length;
    }

}
