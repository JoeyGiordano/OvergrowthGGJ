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
    private string roomName;

    
    //list of rooms connected to current room; current_room -> next_room
    [SerializeField]
    private List<Room> roomConnections = new List<Room>();

    //layout or type of room; enemy, healing, root
    [SerializeField]
    private string layout;

    private RoomStatus roomStatus = RoomStatus.unvisited;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string getRoomName(){
        return roomName;
    }
    public Vector3 getLocation(){
        return transform.position;
    }
    public void setRoomStatus(RoomStatus status){
        roomStatus = status;
    }
}
