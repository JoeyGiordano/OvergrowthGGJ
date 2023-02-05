using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int iFrameAmount;
    [SerializeField] Slider healthBar;

    int curHealth;
    int iFrames;
    SpriteRenderer sp;
    

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        sp = GetComponent<SpriteRenderer>();
        refreshHealthbar();
    }

    // Update is called once per frame
    void Update()
    {

        if(iFrames != 0)
        {
            iFrames--;
        }
        if (iFrames != 0 && iFrames % 5 == 0)
        {
            sp.enabled = !sp.enabled;
        }
        if(iFrames == 0 && !sp.enabled)
        {
            sp.enabled = true;
        }

        if(curHealth <= 0)
        {
            string tag = gameObject.tag;
            if(tag == "Player")
            {
                //Player.die?
                // Debug.Log("Player is Dead :/");
            }
            else if(tag == "Enemy")
            {
                //Destroy enemy object
                Destroy(gameObject);
                return;
            }
            else if(tag == "Spawner")
            {
                //Disable Spawner
                Spawner spawner = this.gameObject.GetComponent<Spawner>();
                spawner.deactivate();
                // Debug.Log("Spawner is Disabled :/");
            }
            else if(tag == "Boss")
            {
                //end the scene
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(iFrames == 0)
        {
            curHealth--;
            iFrames = iFrameAmount;
            if(gameObject.tag == "Boss" && curHealth < maxHealth / 2)
            {
                Boss.Instance.setIsHalfHp(true);
            }
        }
        refreshHealthbar();
    }

    public void ResetHealth()
    {
        curHealth = maxHealth;
    }

    private void refreshHealthbar()
    {
        if (gameObject.tag != "Player") return;
        healthBar.value = (float)curHealth / (float)maxHealth;
    }
}
