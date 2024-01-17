using System;
using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject player;
    public DataLogger dataLogger;
    public GameObject resetScene;
    public int userId;
    public int trialId;
    public TextMeshPro debug;

    private bool isFinish;
    private string[] data;
    private bool isTimeout;

    void Start() {
        isFinish = false;
        isTimeout = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag == "Player") && (isFinish == false) && (isTimeout == false)) 
        {   
            isFinish = true;
            gameObject.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.green);
            data = player.gameObject.GetComponent<TurningControllers>().GameStats();
            dataLogger.StartLogging();
            dataLogger.Log(userId, trialId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), "GiantSlalom", data[0].ToString(), data[1].ToString(), data[2].ToString());
            dataLogger.StopLogging();
            // Set ResetScene flag to true --> Now if u press X next scene will be loaded 
            resetScene.gameObject.GetComponent<ResetScene>().setFinish();
        }
    }

    public void Timeout (){
        isTimeout = true;
    }

    void Update() {
        if (isFinish == true) {
            if (debug) debug.text = "\nFinish!\nTime: "+data[0]+"   Accuracy: "+data[1]+"%   Restored Checkpoints: "+data[2]+"\nPlease remember to give us the feedback for this run\nPress X button to start the next run!";
        }
    }

}
