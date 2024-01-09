using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VelocityHands : MonoBehaviour
{

    public Transform Rhand;
    public Transform Lhand;

    public Vector3 RhandVelocity;
    public Vector3 LhandVelocity;

    private Vector3 LastPositionR;
    private Vector3 LastPositionL;

    // Start is called before the first frame update
    void Start()
    {
        LastPositionL = Lhand.position;
        LastPositionR = Rhand.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // compute difference between last position and current position
        RhandVelocity = (Rhand.position - LastPositionR) / Time.fixedDeltaTime;
        LhandVelocity = (Lhand.position - LastPositionL) / Time.fixedDeltaTime;

        LastPositionL = Lhand.position;
        LastPositionR = Rhand.position;

    }
}
