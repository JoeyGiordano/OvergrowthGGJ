using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1;
    //public Rigidbody2D rb;

    private bool hasKey = false;


    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection.y = -1;
        }
        
        moveDirection.Normalize();
        
        // print(moveSpeed*moveDirection);
        GetComponent<Rigidbody2D>().velocity = moveSpeed * moveDirection;
    }

    public bool getHasKey(){
        return hasKey;
    }
    public void obtainKey(){
        hasKey = true;
    }
}
