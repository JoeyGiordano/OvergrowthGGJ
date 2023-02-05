using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    
    public enum DoorState{
        closedPassword,
        closedMobs,
        closedKey,
        open,
    }
    
    [SerializeField]
    private DoorState doorState;
    
    [SerializeField]
    private Sprite openDoorSprite;

    [SerializeField]
    private Sprite closeDoorSprite;

    DoorState memoryState;
    SpriteRenderer sp;
    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(doorState == DoorState.open){
            sp.sprite = openDoorSprite;
            col.enabled = false;
        }
        else
        {
            sp.sprite = closeDoorSprite;
            col.enabled = true;
        }
    }

    public void PasswordUnlock(){
        doorState = DoorState.open;
    }



    public void OnCollisionEnter2D(Collision2D other){
        GameObject player = GameObject.Find("Player");
        if(other.gameObject == player){
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm.getHasKey()){
                if(doorState == DoorState.closedKey){
                    doorState = DoorState.open;
                    pm.useKey();
                }
            }
        }
    }

    public void ActivateMobLock()
    {
        memoryState = doorState;
        doorState = DoorState.closedMobs;
    }

    public void DeactivateMobLock()
    {
        doorState = memoryState;
    }
}
