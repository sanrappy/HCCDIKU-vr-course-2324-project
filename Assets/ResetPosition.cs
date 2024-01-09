using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 initialPosition;
    void Start()
    {   
        print("Initial position saved!");
        initialPosition = this.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four)) 
        {
            this.GetComponent<Transform>().position = initialPosition;
        }
    }
}
