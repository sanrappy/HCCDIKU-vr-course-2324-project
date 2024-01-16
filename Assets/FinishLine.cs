using System;
using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject player;
    public DataLogger dataLogger;
    public int userId;
    public int trialId;
    public TextMeshPro debug;

    private bool isFinish;
    private string[] data;

    void Start() {
        isFinish = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") 
        {   
            isFinish = true;
            gameObject.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.green);
            data = player.gameObject.GetComponent<TurningControllers>().GameStats();
            dataLogger.StartLogging();
            dataLogger.Log(userId, trialId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), "GiantSlalom", data[0].ToString(), data[1].ToString(), data[2].ToString());
            dataLogger.StopLogging();
        }
    }

    void Update() {
        if (isFinish == true) {
            if (debug) debug.text = "\nFinish!\nTime: "+data[0]+"   Accuracy: "+data[1]+"   Restored Checkpoints: "+data[2]+"%\nPress X button to start the next race!";
        }
    }

}