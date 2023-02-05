using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameObject target;
    [SerializeField] string enemyType;

    [SerializeField] float flyAccel, flyAccelNoise, flyMaxSpeed;
    [SerializeField] float spiderJumpCooldown, spiderJumpDist, spiderJumpStddv;
    [SerializeField] float wormCooldown = 20, wormForecast = 2, wormAttack = 1;

    bool spiderJumping = false;
    float spiderNextY;

    float wormState = 0, wormCounter = 0;

    float savedTime;

    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        Vector3 unitTowardsTarget = (target.transform.position - transform.position).normalized;

        if (enemyType == "fly")
        {
            rb.AddForce(unitTowardsTarget * flyAccel + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1),0).normalized*flyAccel*flyAccelNoise);
            if (rb.velocity.magnitude > flyMaxSpeed)
            {
                rb.velocity = rb.velocity.normalized * flyMaxSpeed;
            }
        }
        if (enemyType == "spider")
        {
            if (spiderJumping && spiderNextY > transform.position.y && rb.velocity.y < 0) //spider done jumping
            {
                spiderJumping = false;
                savedTime = Time.time;
                rb.gravityScale = 0;
                rb.velocity = new Vector3(0, 0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                //landing anim
            }
            else if (!spiderJumping && savedTime + spiderJumpCooldown < Time.time)    //if the cooldown is over
            {
                spiderJumping = true;
                Vector2 rand = Random.insideUnitCircle.normalized;
                Vector3 endLocation = new Vector3(unitTowardsTarget.x, unitTowardsTarget.y * 2 / 3, 0) * spiderJumpDist + new Vector3(rand.x, rand.y*2/3, 0) * spiderJumpStddv;
                spiderNextY = transform.position.y + endLocation.y;
                rb.velocity = spiderJumpDist * new Vector3(unitTowardsTarget.x, 2 + unitTowardsTarget.y * 2 / 3, 0);
                rb.gravityScale = 4;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //jumping anim
            }
        }
        if (enemyType == "worm")
        {
            if (wormState == 0)
            {
                wormCounter += Random.Range(0, 1);
                if (wormCounter > wormCooldown)
                {
                    wormState = 1;
                    wormCounter = 0;
                    savedTime = Time.time;
                    //show dirt forecast, damage remains off
                }
            } else if (wormState == 1)
            {
                if (savedTime + wormForecast < Time.time)
                {
                    wormState = 2;
                    savedTime = Time.time;
                    //show attack anim, turn damage on
                }
            } else if (wormState == 2)
            {
                if (savedTime + wormAttack < Time.time)
                {
                    wormState = 0;
                    //move to a random position in the room
                    //hide, turn damage off
                }
            }
        }
    }
}
