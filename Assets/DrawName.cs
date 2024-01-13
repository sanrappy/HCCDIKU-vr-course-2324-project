using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawName : MonoBehaviour
{
    public DataLogger dataLogger;
    public int userId = 1;
    public int trialId = 1;
    public string eventName = "Log";
    public float factor;
    
    private Vector3 oldPosition; 
    private bool isLogging = false;

    void Start()
    {
        oldPosition = this.gameObject.GetComponent<Transform>().eulerAngles;
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
                dataLogger.Log(userId,trialId,DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),eventName,oldPosition.x.ToString(),oldPosition.y.ToString(), oldPosition.z.ToString());
            } else {
                isLogging = false;
                print("Stop Logging");
                dataLogger.StopLogging();
            }
        }
    }

    void FixedUpdate() // TODO: Simplify by using a counter to log only every n update 
    {
        Vector3 position = this.gameObject.GetComponent<Transform>().eulerAngles;
        var dis = Vector3.Distance(position, oldPosition);
        if (dis > factor) { 
            dataLogger.Log(userId,trialId,DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),eventName,position.x.ToString(),position.y.ToString(), position.z.ToString());
            oldPosition = position;
        }
    }
}
