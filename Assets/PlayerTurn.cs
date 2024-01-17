using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public GameObject player;
    public int flagId;

    private bool isDone;

    void Start(){
        isDone = false;
    }

    void OnTriggerEnter(Collider collider){
        if((collider.gameObject.tag == "Player") && (isDone == false)){
            isDone = true;
            player.GetComponent<PointsGame>().AddPoint();
            // update checkpoint
            player.GetComponent<TurningControllers>().UpdateCheckpoint(this.gameObject.transform.position);
        }
    }
}

