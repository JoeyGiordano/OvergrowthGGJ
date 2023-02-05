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

    DoorState memoryState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doorState == DoorState.open){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void PasswordUnlock(){
        doorState = DoorState.open;
    }



    public void OnCollisionEnter2D(Collision2D other){
        GameObject player = GameObject.Find("Player");
        if(other.gameObject == player){
            if(player.GetComponent<PlayerMovement>().getHasKey()){
                if(doorState == DoorState.closedKey){
                    doorState = DoorState.open;
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
