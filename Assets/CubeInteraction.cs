using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeInteraction : MonoBehaviour
{

    private float forceFactor = 100;

    public GameObject Player;

    void OnTriggerEnter(Collider other) {

        print("Trigger: "+other.gameObject.name);

        if (other.gameObject.tag == "HandR") {
            
            //print("Hand trigger");
            // add random force to this gameobject 
            //this.GetComponent<Rigidbody>().AddForce(transform.forward * 1000 * forceFactor);

            Vector3 RhandVelocity = Player.GetComponent<VelocityHands>().RhandVelocity;

            print("Right Controller Velocity: "+RhandVelocity);
            // compute you own velocity if not working by taking initial and final position hand 
            this.GetComponent<Rigidbody>().AddForce(RhandVelocity*forceFactor);

        } else if (other.gameObject.tag == "HandL"){

            Vector3 LhandVelocity = Player.GetComponent<VelocityHands>().LhandVelocity;

            print("Left Controller Velocity: "+LhandVelocity);
            this.GetComponent<Rigidbody>().AddForce(LhandVelocity*forceFactor);

        }

    } 

}
