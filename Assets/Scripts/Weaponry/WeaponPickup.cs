using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private WeaponHandler weaponHandler;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D test)
    {
        if (test.tag == "Player")
        {
            weaponHandler = test.GetComponent<WeaponHandler>();
            var weaponObj = Instantiate(weapon, weaponHandler.weaponTransform.position, Quaternion.identity, weaponHandler.gameObject.transform);
            var weaponScript = weaponObj.GetComponent<Weapon>();
            weaponHandler.weapons.Add(weaponScript);
            weaponHandler.ChangeWeapon();
            weaponHandler.hasWeapon = true;
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
