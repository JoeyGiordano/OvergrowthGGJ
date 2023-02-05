using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    private float gunHeat = 0f;
    public float bulletForce = 15f;

    private GameObject player;
    
    // Update is called once per frame

    void Start(){
        player = GameObject.Find("Player");
    }
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
        //bullet_rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Vector3 bullet_velocity = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.transform.position;
        bullet_rb.velocity = bullet_velocity.normalized * bulletForce;
        
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), bullet.GetComponent<BoxCollider2D>());
        Destroy(bullet, 3f);
        Destroy(bullet_rb, 3f);
    }
}
