using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Connection to terminal
    [SerializeField] GameObject terminalObject;
    [SerializeField] Terminal terminal;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float aveCooldown, cooldownStddv, initCooldown;
    [SerializeField] int spawnCap;

    float timeOfLastSpawn;
    float cooldown;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        // activate();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && Time.time > timeOfLastSpawn + cooldown)
        {
            spawn();
            timeOfLastSpawn = Time.time;
            cooldown = aveCooldown + cooldownStddv*Random.insideUnitSphere.x;
        }
    }

    private bool spawn()
    {
        if (GetComponentsInChildren<Transform>().Length >= spawnCap)
        {
            return false;
        }
        GameObject e = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        e.transform.parent = transform;
        return true;
    }

    public bool getStatus(){
        return active;
    }
    public void activate()
    {
        timeOfLastSpawn = Time.time;
        cooldown = 0;
        active = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
    public void deactivate(){
        if (active)
        {
            string[] message = { gameObject.name + " has been disabled" };
            terminalObject.SetActive(true);
            terminal.addToQueue(message);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        active = false;
    }

}
