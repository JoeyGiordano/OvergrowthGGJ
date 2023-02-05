using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    // Maps tags to how the damage source should respond
    [SerializeField] List<string> collisionKeys;
    [SerializeField] List<string> collisionActions;
    [SerializeField] int damageAmount;
    [SerializeField] bool destroyOnDamage;

    private void HandleCollision(GameObject g)
    {
        string tag = g.tag;
        string action = "";
        for(int i = 0; i < collisionKeys.Count; i++)
        {
            if(collisionKeys[i] == tag)
            {
                action = collisionActions[i];
                break;
            }

        }
        switch (action)
        {
            case ("damage"):
                g.SendMessage("TakeDamage", damageAmount);
                if(destroyOnDamage)
                {
                    Destroy(gameObject);
                    return;
                }
                break;
            case ("destroy"):
                Destroy(gameObject);
                return;
            case ("ignore"):
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }
    // public void setPiercing(bool piercing){
    //     destroyOnDamage = piercing;
    // }
}
