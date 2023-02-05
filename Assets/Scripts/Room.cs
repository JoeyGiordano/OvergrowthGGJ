using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Connection to terminal
    [SerializeField] GameObject terminalObject;
    [SerializeField] Terminal terminal;

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
    public string getRoomName(){
        return roomName;
    }
    public Vector3 getLocation(){
        return transform.position;
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
    private void OnTriggerEnter2D(Collider2D other){
        // print(roomStatus);
        if(other.gameObject == player.gameObject){
            string[] message1 = { "Welcome to " + roomName };
            terminalObject.SetActive(true);
            terminal.addToQueue(message1);
            insideRoom = true;
            //print("entering room");
            if(roomStatus == RoomStatus.unvisited){
                foreach (Transform child in transform)
                {
                    Spawner spawner = child.gameObject.GetComponent<Spawner>();
                    if (spawner != null)
                    {
                        //print("updated game status: " + roomStatus.ToString());
                        //print(spawner);
                        if (roomStatus != RoomStatus.cleared)
                        {
                            //print("calling activate");
                            spawner.activate();
                        }
                    }
                }
                if (transform.childCount != 0)
                {
                    roomStatus = RoomStatus.infected;
                    string[] message2 = { roomName + " is infected by the Virus!" };
                    terminalObject.SetActive(true);
                    terminal.addToQueue(message2);
                }
            }
            //print("game status: " + roomStatus.ToString());
            
            
            int reinfect_chance = Random.Range(0, 100);
            if(roomStatus == RoomStatus.cleared && reinfect_chance < 20 && transform.childCount != 0){
                LevelManager.Instance.reinfectRoom(roomName);
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
        foreach (Transform child in transform)
        {
            Spawner spawner = child.gameObject.GetComponent<Spawner>();
            if (spawner != null && spawner.active)
            {
                return;
            }
        }
        if (insideRoom && roomStatus != RoomStatus.cleared)
        {
            roomStatus = RoomStatus.cleared;
            CommandManager.Instance.addToAvailableRooms(roomName);
            if (transform.childCount != 0) {
                string[] message = { "You've cleared the Virus from " + roomName + "!" };
                terminalObject.SetActive(true);
                terminal.addToQueue(message);
            }
        }
    }
    
}
