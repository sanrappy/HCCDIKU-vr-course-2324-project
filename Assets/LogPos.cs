using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogPos : MonoBehaviour
{
    public DataLogger dataLogger;
    public float factor;
    
    private Vector3 oldPosition; 
    private bool isLogging = false;

    void Start()
    {
        oldPosition = this.gameObject.GetComponent<Transform>().position;
        print("Initial Position: "+oldPosition.x.ToString()+";"+oldPosition.y.ToString()+";"+oldPosition.z.ToString());
    }

    void Update() // TODO: Use keyboard button press to start/stop log
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isLogging == false) {
                isLogging = true;
                print("Start Logging");
                dataLogger.StartLogging();
                dataLogger.Log(0,0,DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),"Test",oldPosition.x.ToString(),oldPosition.y.ToString(), oldPosition.z.ToString());
            } else {
                isLogging = false;
                print("Stop Logging");
                dataLogger.StopLogging();
            }
        }
    }

    void FixedUpdate() // TODO: Simplify by using a counter to log only every n update 
    {
        Vector3 position = this.gameObject.GetComponent<Transform>().position;
        var dis = Vector3.Distance(position, oldPosition);
        if (dis > factor) { 
            dataLogger.Log(0,0,DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),"Test",position.x.ToString(),position.y.ToString(), position.z.ToString());
            oldPosition = position;
        }
    }
}
