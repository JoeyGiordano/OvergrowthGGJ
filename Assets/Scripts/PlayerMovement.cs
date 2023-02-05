using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1.5f;
    public Animator animator;
    Rigidbody2D rb;
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

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirection += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveDirection += new Vector3(1, 0, 0);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveDirection += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveDirection += new Vector3(0, -1, 0);
            }
            
            moveDirection.Normalize();
            
            // print(moveSpeed*moveDirection);
            GetComponent<Rigidbody2D>().velocity = moveSpeed * moveDirection;

            //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // Sprite animations for Animator
            // //fix animation later
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
