using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    //public Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

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
        GetComponent<Rigidbody2D>().velocity = moveSpeed * moveDirection;
    }
}
