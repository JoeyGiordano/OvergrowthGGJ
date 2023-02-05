using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1.5f;
    public Animator animator;
    public Rigidbody2D rb;
    Vector2 mousePos;

    private bool isAllowedToMove = true;
    //bool isShooting = true;
    //public Camera cam;

    

    private void Start() 
    {
        animator = GetComponent<Animator>();
        
    }
    private bool hasKey = false;


    // Update is called once per frame
    private void Update()
    {
        if(isAllowedToMove){
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

            //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // Sprite animations for Animator
            //fix animation later
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
        

    }

    void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
    }

    /*void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }*/


    public bool getHasKey(){
        return hasKey;
    }
    public void obtainKey(){
        hasKey = true;
    }

    public void OnCollisionEnter2D(Collision2D collision){
        // if (collision.gameObject.tag == "Projectile"){
        //     Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), collision.collider);
        // }
    }
    public void restrictMovement(bool restrict){
        isAllowedToMove = !restrict;
    }
}
