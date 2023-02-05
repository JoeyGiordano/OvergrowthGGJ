using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D collider;
    bool hasKey = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasKey)
        {
            animator.SetFloat("HasKey",5f);
            collider.enabled = false;
        }
    }
}
