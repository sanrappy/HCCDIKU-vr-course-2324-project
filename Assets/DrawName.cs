using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawName : MonoBehaviour
{
    public DataLogger dataLogger;
    public float factor;
    
    private Vector3 oldPosition; 
    private bool isLogging = false;

    void Start()
    {
        oldPosition = this.gameObject.GetComponent<Transform>().localPosition;
        print("Initial Position: "+oldPosition.x.ToString()+";"+oldPosition.y.ToString()+";"+oldPosition.z.ToString());
    }

    void Update()
    {   
        if (OVRInput.GetDown(OVRInput.Button.One)==true)
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
        Vector3 position = this.gameObject.GetComponent<Transform>().localPosition;
        var dis = Vector3.Distance(position, oldPosition);
        if (dis > factor) { 
            dataLogger.Log(0,0,DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),"Test",position.x.ToString(),position.y.ToString(), position.z.ToString());
            oldPosition = position;
        }
    }
}
