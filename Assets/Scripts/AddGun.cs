using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGun : MonoBehaviour
{
    [SerializeField] Shooting weaponHandler;
    [SerializeField] string gunName;
    
    private void OnTriggerEnter2D()
    {
        weaponHandler.addWeapon(gunName);
        Destroy(gameObject);
    }
}
