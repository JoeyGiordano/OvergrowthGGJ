using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other){
        print(other);
        GameObject player = GameObject.Find("Player");
        if(other.gameObject == player){
            player.GetComponent<PlayerMovement>().obtainKey();
            this.gameObject.SetActive(false);
        }
        
        
    }
}
