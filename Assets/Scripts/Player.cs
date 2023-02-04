using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += new Vector3(-1, 0, 0);
        }

        transform.position += movement * speed * Time.deltaTime;

    }
}
