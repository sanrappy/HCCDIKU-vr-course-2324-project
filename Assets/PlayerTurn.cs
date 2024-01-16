using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public GameObject player;
    public int flagId;

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player"){
            player.GetComponent<PointsGame>().AddPoint();
            // update checkpoint
            player.GetComponent<TurningControllers>().UpdateCheckpoint(this.gameObject.transform.position);
        }
    }
}

