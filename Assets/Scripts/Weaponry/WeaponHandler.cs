using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Player player;
    public Transform weaponTransform;
    public bool hasWeapon;

    public List<Weapon> weapons = new List<Weapon>();

    public Weapon currentWeapon;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (weapons.Count != 0)
            currentWeapon = weapons[index];
    }

    // Update is called once per frame
    void Update()
    {
        CheckWeapon();
    }
    public void SetCurrentWeapon()
    {

    }
    public void ChangeWeapon()
    {
        if(index == weapons.Count - 1)
            index = 0;
        else
            index++;

        currentWeapon = weapons[index];
        currentWeapon.lightAnim.StopPlayback();
        player.attackPos = currentWeapon.attackPos;
    }
    public bool CheckWeapon()
    {
        return hasWeapon = weapons.Count != 0;
    }
}
