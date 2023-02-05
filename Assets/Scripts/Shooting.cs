using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Connection to terminal
    [SerializeField] GameObject terminalObject;
    [SerializeField] Terminal terminal;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] numbers;
    [SerializeField] float bulletForce = 15f;

    [SerializeField] string weaponType = "terminate-pellets";
    float gunHeat = 0f;
    GameObject player;

    Hashtable weaponUnlocks;
    
    // Update is called once per frame

    void Start()
    {
        player = GameObject.Find("Player");
        terminalObject = GameObject.Find("Terminal");
        terminal = GameObject.Find("TextScrollManager").GetComponent<Terminal>();

        weaponUnlocks = new Hashtable();
        weaponUnlocks.Add("terminate-pellets", true);
        weaponUnlocks.Add("number-pellets", false);
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

    public void setWeapon(string weapon){
        if ((bool) weaponUnlocks[weapon] == true)
        {
            weaponType = weapon;
            string[] message = { weapon + " has been equipped" };
            terminalObject.SetActive(true);
            terminal.addToQueue(message);
        }
        else
        {
            string[] message = { weapon + " not yet unlocked" };
            terminalObject.SetActive(true);
            terminal.addToQueue(message);
        }
    }

    void Shoot()
    {
        GameObject bullet;
        Rigidbody2D bullet_rb;
        Vector3 bullet_velocity;
        // print(weaponType);
        switch (weaponType)
        {   
            case ("number-pellets"):
                // print("using numbers");
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

    public void addWeapon(string name)
    {
        weaponUnlocks[name] = true;
    }
}
