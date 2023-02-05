using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    private float gunHeat = 0f;
    public float bulletForce = 5f;

    
    // Update is called once per frame
    void Update()
    {
        // continuous firing?
        if(gunHeat <= 0)
        {
            Shoot();
            gunHeat = 0.20f;
        } 
        else
        {
            gunHeat -= Time.deltaTime;
        }
        
    }

    void Shoot()
    {
        // spawn bullet, and add a force to the rigid body to make it fly
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
        bullet_rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 1f);
        Destroy(bullet_rb, 1f);
    }
}
