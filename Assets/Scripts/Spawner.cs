using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float aveCooldown, cooldownStddv, initCooldown;
    [SerializeField] int spawnCap;

    float timeOfLastSpawn;
    float cooldown;
    bool active;

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

    public void activate()
    {
        timeOfLastSpawn = Time.time;
        cooldown = initCooldown;
        active = true;
    }
    public void deactivate(){
        active = false;
    }

}
