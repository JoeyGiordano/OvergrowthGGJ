using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    //different statuses of the room:
    //1. unvisited: the player has not yet reached this room
    //2. infected: the player has just visited this room or the virus re-infects this room.
    //3. cleared: the player has killed all of the enemies in the room.

    //healing rooms should not be able to be infected
    public enum RoomStatus{
        unvisited,
        infected,
        cleared,

    }

    //name of current room
    [SerializeField]
    private string roomName = "";

    
    //list of rooms connected to current room; current_room -> next_room
    [SerializeField]
    private List<Room> roomConnections = new List<Room>();

    //layout or type of room; enemy, healing, root
    [SerializeField]
    private string layout;

    public RoomStatus roomStatus;
    private bool insideRoom = false;
    GameObject player;
    private bool loopLock = false;

    // Start is called before the first frame update
    void Start()
    {
        roomStatus = RoomStatus.unvisited;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // if(roomName == "src"){
        //     playerIsInRoom();
        // }
        // print(roomStatus);
        setRoomStatus(determineRoomState());
    }
    public string getRoomName(){
        return roomName;
    }
    public Vector3 getLocation(){
        return transform.position;
    }
    public void setRoomStatus(RoomStatus status){
        roomStatus = status;
        // if(status == RoomStatus.cleared){
        //     CameraController.Instance.setPlayerPositionState(CameraController.PlayerPositionState.PlayerIsInHallway, player.transform);
        // }
    }

    public void resetEnemies(){
        roomStatus = RoomStatus.unvisited;
        CommandManager.Instance.removeAvailableRoom(roomName);
        foreach(Transform child in transform){
            Spawner spawner = child.gameObject.GetComponent<Spawner>();
            if(spawner != null){
                // print(spawner);
                spawner.GetComponent<Health>().ResetHealth();
                spawner.activate();
            }
        }
    }
    private RoomStatus determineRoomState(){

        if(roomStatus == RoomStatus.unvisited){
            return RoomStatus.unvisited;
        }
        foreach(Transform child in transform){
            Spawner spawner = child.gameObject.GetComponent<Spawner>();
            if(spawner != null){
                if(!spawner.getStatus()){
                    //do nothing
                }
                else{
                    return RoomStatus.infected;
                }
            }
        }
        // print("room is clear");
        CommandManager.Instance.addToAvailableRooms(roomName);
        // print("cleared room");
        return RoomStatus.cleared;
    }
    private void OnTriggerEnter2D(Collider2D other){
        // print(roomStatus);
        if(other.gameObject == player.gameObject){
            insideRoom = true;
            print("entering room");
            if(roomStatus == RoomStatus.unvisited){
                roomStatus = RoomStatus.infected;
            }
            print("game status: " + roomStatus.ToString());

            int child_count = 0;
            foreach(Transform child in transform){
                child_count += 1;
                Spawner spawner = child.gameObject.GetComponent<Spawner>();
                if(spawner != null){
                    print("updated game status: " + roomStatus.ToString());
                    print(spawner);
                    if(roomStatus != RoomStatus.cleared){   
                        print("calling activate");
                        spawner.activate();
                    }
                    else{
                        spawner.deactivate();
                        print("deactivating?");
                    }
                }
            }
            if(child_count == 0){
                roomStatus = RoomStatus.cleared;
            }
            int reinfect_chance = Random.Range(0, 100);
            if(reinfect_chance < 80){
                LevelManager.Instance.reinfectRoom();
        }
        }
        
        
    }
    private void OnTriggerLeave2D(Collider2D other){
        if(other.gameObject == player.gameObject){
            insideRoom = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other){
        // print(roomStatus);
        if(Input.GetKey(KeyCode.L)){
            setRoomStatus(RoomStatus.cleared);
        }
    }
    
}
