using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class Terminal : MonoBehaviour
{
    /*
     * This class is a generic text scroller class for Unity. All of the important components
     * have been abstracted and can be sent in as input through the Unity editor. Most of the
     * underhead has been commented as well, so if you want to look into it and change it up,
     * go for it. If there are any sort of issues that you come accross, feel free to ask me
     * (if you have access to me) how to fix it.
     * 
     * Author: Ian "Gaming" Harvey
     */
    
        // This field represents the speed at which the text scrolls accross the screen
    [SerializeField] int framesPerCharacter;


        // This field represents the physical text that will appear on the screen
        // Every addtional entry into the list represents a new line in the queue
        // Note that the buffer will run the second that the script is enabled
        // The lines are listed top down
    [SerializeField] string[] lineQueueBuffer;
    Queue<string> lineQueue;


        // Optional Field (if you want your text scrolling to have a game-ey feel to it)
    [SerializeField] AudioSource tick;


        // This field represents an external component you will need to attach to the object this script is attached to
    [SerializeField] GameObject[] textObjects;


        // This field just represents the terminal object so that the script can disable it...
    [SerializeField] GameObject terminalObject;

    
        // All variables from here and below are not important to your understanding of how to use this script
    string atWrd;
    int at;
    string curWord;
    bool active;
    bool end;
    bool reading;
    int timeToUpdate;
    List<TextMeshPro> texts;
    List<Transform> textPositions;
    int atTxt;
    TextMeshPro displayedText;
    bool writing;
    float startTime;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");

        lineQueue = new Queue<string>();
        foreach (string s in lineQueueBuffer)
        {
            lineQueue.Enqueue(s);
        }
        if(lineQueue.Count != 0)
        {
            reading = true;
        }
        texts = new List<TextMeshPro>();
        textPositions = new List<Transform>();
        foreach(GameObject g in textObjects)
        {
            texts.Add(g.GetComponent<TextMeshPro>());
            textPositions.Add(g.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Read from queue if reading.
        if (reading)
        {
            if (!active)
            {
                if (lineQueue.Count == 0)
                {
                    reading = false;
                    startTime = Time.time;
                }
                else
                {
                    // Takes next word and shifts up other lines when necessary
                    atWrd = lineQueue.Dequeue();
                    shiftLines();
                    active = true;
                    timeToUpdate = framesPerCharacter;
                    at = 0;
                    curWord = "";
                    if (atWrd.Contains('$'))
                    {
                        at = atWrd.IndexOf('$');
                        curWord = atWrd.Substring(0, at);
                    }
                }
            }
            else
            {
                // Progresses the text scrolling
                timeToUpdate--;
                if (timeToUpdate == 0)
                {
                    // Update the scrolling text

                    char toAdd = atWrd[at];
                    at++;
                    timeToUpdate = framesPerCharacter;
                    curWord += toAdd;
                    // Play the sound if you added it
                    if (tick != null && !tick.isPlaying)
                        tick.Play();
                    displayedText.text = curWord;
                }

                // End scrolling if no more chars left
                if (at == atWrd.Length)
                {
                    active = false;
                }
            }
        }
        else if(writing)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {

                writing = false;
                if (curWord == "")
                {
                    terminalObject.SetActive(false);
                    return;
                }

                player.GetComponent<PlayerMovement>().restrictMovement(false);
                // You can figure out what you want to do with the command here...

                int execute = CommandManager.Instance.parseCommandLine(curWord);
                string executeRet = "";

                switch (execute){
                    case -1: 
                        executeRet = "[Errno -1] Invalid Syntax!";
                        break;
                    case 1:
                        executeRet = "[Errno 1]: Incorrect Number of Arguments!";
                        break;
                    case 2:
                        executeRet = "[Errno 2]: Argument(s) Not Allowed!";
                        break;
                    default:
                        break;
                }
                print(executeRet);
                if(executeRet != "")
                    lineQueue.Enqueue(executeRet);
                reading = true;

                //writing = false, reading = true

                startTime = Time.time;
            }
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                    if (curWord.Length != 0)
                    {
                        curWord = curWord.Substring(0, curWord.Length - 1);
                    }
                }
                else if(curWord.Length <= 40)
                {
                    curWord += c;
                }
            }
            displayedText.text = "C:Alex:-$ " + curWord;
        }
        else if (Time.time - startTime >= 3)
        {
            terminalObject.SetActive(false);
        }
    }

    private void shiftLines()
    {
        displayedText = texts[atTxt % texts.Count];
        displayedText.text = "";
        if (atTxt >= texts.Count)
        {
            if (atTxt == 2 * texts.Count)
                atTxt = texts.Count;
            foreach (Transform t in textPositions)
            {
                t.position = new Vector3(t.parent.position.x, t.position.y + 2, t.position.z);
            }
            textPositions[atTxt % texts.Count].position = new Vector3(textPositions[atTxt % texts.Count].parent.position.x, -2.5F, textPositions[atTxt % texts.Count].position.z);
        }
        atTxt++; 
    }

    // Spawns new lines in the terminal in accordance with the array sent in
    public void addToQueue(string[] nextLines)
    {
        foreach (string s in nextLines) {
            lineQueue.Enqueue(s);
        }
        // at = 0;
        reading = true;
    }

    public void beginWriting()
    {
        if (!reading)
        {
            if (!writing)
            {
                shiftLines();
                curWord = "";
            }
            player.GetComponent<PlayerMovement>().restrictMovement(true);
            writing = true;
        }
    }
}
