using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] numbers;
    [SerializeField] float bulletForce = 15f;

    [SerializeField] string weaponType;
    float gunHeat = 0f;
    GameObject player;
    
    // Update is called once per frame

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // continuous firing?
        if(gunHeat <= 0)
        {
            gunHeat = 0.20f;
            Shoot();
        } 
        else
        {
            gunHeat -= Time.deltaTime;
        }
        
    }

    void Shoot()
    {
        GameObject bullet;
        Rigidbody2D bullet_rb;
        Vector3 bullet_velocity;
        switch (weaponType)
        {
            case ("number"):
                // spawn bullet, and add a force to the rigid body to make it fly
                bullet = Instantiate(numbers[Random.Range(0, 10)], firePoint.position, Quaternion.identity);
                bullet_rb = bullet.GetComponent<Rigidbody2D>();
                bullet_velocity = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.transform.position;
                bullet_rb.velocity = bullet_velocity.normalized * bulletForce;
                break;
            default:
                // spawn bullet, and add a force to the rigid body to make it fly
                bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet_rb = bullet.GetComponent<Rigidbody2D>();
                bullet_velocity = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.transform.position;
                bullet_rb.velocity = bullet_velocity.normalized * bulletForce;
                break;
        }
        
        //Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), bullet.GetComponent<BoxCollider2D>());
        //Destroy(bullet, 3f);
        //Destroy(bullet_rb, 3f);
    }
}
