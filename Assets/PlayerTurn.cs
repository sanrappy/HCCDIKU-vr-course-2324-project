using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player"){
            player.GetComponent<PointsGame>().AddPoint();
            // update checkpoint
        }
    }
}

