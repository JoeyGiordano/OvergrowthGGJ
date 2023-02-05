using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingActivation : MonoBehaviour
{
    [SerializeField] GameObject terminalObject;
    [SerializeField] Terminal terminalWriter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Slash))
        {
            terminalObject.SetActive(true);
            terminalWriter.beginWriting();
        }
    }
}
