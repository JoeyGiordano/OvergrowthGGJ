using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{

    #region Singleton
    private static CommandManager _CommandManager;

    public static CommandManager Instance { get { return _CommandManager; } }


    private void Awake()
    {
        if (_CommandManager != null && _CommandManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _CommandManager= this;
        }
    }
    #endregion 

    private List<string> allowedCommands = new List<string> {
        "cd",
        "ls",
        "exec"
    };
    private List<string> allowedLocations = new List<string>();

    private List<string> availableLocations = new List<string>();
    private List<string> allowedWeapons = new List<string>();

    private bool devState = false;

    GameObject terminal;

    // Start is called before the first frame update
    void Start()
    {
        terminal = GameObject.Find("TextScrollManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(!devState){
            setupDevTest();
        }

        if(devState && Input.GetKeyUp(KeyCode.K)){
            print("calling parseCommandLine()");
            parseCommandLine("cd r1");
        }
        if(devState && Input.GetKeyUp(KeyCode.O)){
            print("calling parseCommandLine()");
            parseCommandLine("cd r7");
        }
        if(devState && Input.GetKeyUp(KeyCode.I)){
            print("calling parseCommandLine()");
            parseCommandLine("cd src");
        }
    }
    
    private void setupDevTest(){
        LevelManager lm = LevelManager.Instance;
        foreach(Room r in lm.getRooms()){
            // if(r.getRoomName() == "src"){
            //     r.setRoomStatus(Room.RoomStatus.cleared);
            // }
        }
        allowedLocations = lm.getRoomNames();
        availableLocations.Add("Documents");
        allowedWeapons.Add("gun");
        allowedWeapons.Add("knife");
        devState = true;
    }

    //returns:
    //-1: no command available or stated command is not valid (like mkdir)
    //return of execute(): otherwise
    public int parseCommandLine(string line){
        string[] line_split_by_whitespace = line.Split(" ");

        if(line_split_by_whitespace.Length == 0){
            print("not enough arguments");
            return -1;
        }
        else{
            if(allowedCommands.Contains(line_split_by_whitespace[0])){
                string command = line_split_by_whitespace[0];
                List<string> arguments = new List<string>();
                if(line_split_by_whitespace.Length > 1){
                    for(int i=1; i < line_split_by_whitespace.Length; i++){
                        arguments.Add(line_split_by_whitespace[i]);
                    }
                } 
                print("calling execute()");
                return execute(command, arguments);
            }
            else {
                print("command not allowed");
                return -1;
            }
        }

    }

    //returns:
    //0 - proper execution
    //1 - invalid number of arguments
    //2 - invalid arguments
    private int execute(string command, List<string> arguments){
        switch(command){
            case "cd":
                if(arguments.Count!= 1){
                    print("cd: not enough arguments");
                    return 1;
                }
                if(!allowedLocations.Contains(arguments[0])){
                    print("argument not allowed");
                    return 2;
                }
                LevelManager.Instance.teleport(arguments[0]);
                return 0;
            case "ls":
                if(arguments.Count > 0){
                    return 1;
                }
                string[] availableLocationsArr;

                if(availableLocations.Count <= 0){
                    print("no locations");
                    availableLocationsArr = new string[] {"No locations cleared!"};
                }
                else{
                    
                    List<string> groupedLocations = new List<string>();

                    int locationGroupCount = 4;
                    int counter = 0;
                    string appendLocation = "";
                    for(int i = 0; i < availableLocations.Count; i++){
                        if(counter == 0){
                            appendLocation = availableLocations[i];
                        }
                        else if(counter == locationGroupCount){
                            groupedLocations.Add(appendLocation);
                            appendLocation = availableLocations[i];
                            counter = 0;
                        }
                        else{
                            appendLocation += "  " + availableLocations[i];
                        }
                        counter += 1;
                    }
                    groupedLocations.Add(appendLocation);
                    availableLocationsArr = groupedLocations.ToArray();
                }

                terminal.GetComponent<Terminal>().addToQueue(availableLocationsArr);
                //print all available rooms here
                return 0;
            case "exec":
                if(arguments.Count != 1){
                    return 1;
                }
                if(!allowedLocations.Contains(arguments[0])){
                    return 2;
                }

                //change player weapon here
                return 0;
            default:
                return 1;
        }
    } 
}
