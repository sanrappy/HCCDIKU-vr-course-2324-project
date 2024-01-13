using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TurningControllers : MonoBehaviour
{

    public Transform leftController;
    public Transform rightController;
    public TextMeshPro debug;
    public float smooth; // Map controller rotation angle to the player // TODO: use a fucntion instead
    public int minRotation; // Reduce Noise

    private Rigidbody rb;
    private float angle;
    private bool isStop = false; // Implement the STOP mechanism
    private bool isCalibrate = false; // Calibrate the controllers rotation
    private float initialLeftControllerRotation; // Set by calibration
    private float initialRightControllerRotation; // Set by calibration
    private Vector3 direction;
    private Vector3 lookDirection;
    private Vector3 stoppedDirection;

    // Start is called before the first frame update
    void  Start()
    {   
        minRotation = Math.Max(15,minRotation);
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isCalibrate == false && OVRInput.GetDown(OVRInput.Button.One)){
            // do calibrate
            initialLeftControllerRotation = leftController.eulerAngles.z;
            initialRightControllerRotation = rightController.eulerAngles.z;    
            isCalibrate = true;
            rb.useGravity = true;
        }

        if (isCalibrate == true) {
            
            float leftControllerRotation = leftController.eulerAngles.z - initialLeftControllerRotation;
            float rightControllerRotation = rightController.eulerAngles.z - initialRightControllerRotation;
            
            debug.text = "Left Controller Angle: " + leftControllerRotation + "\nRight Controller Angle: " + rightControllerRotation;

            if (leftControllerRotation < 180 && leftControllerRotation > minRotation) {
                
                // Get the new forward direction after rotation
                angle = leftControllerRotation;

                debug.text = debug.text + "\nLeft Turn Direction: " + angle;

            } else if (rightControllerRotation > -160 && Math.Abs(rightControllerRotation) > minRotation) {
                
                // Get the new forward direction after rotation
                angle = rightControllerRotation;

                debug.text = debug.text + "\nRight Turn Direction: " + angle;

            } else {
                angle = 0;
            }
            
            if (angle != 0) {

                if (Math.Abs(angle) > 70) {
                    //debug.text = debug.text + "\nSTOP";
                    isStop = true;
                    stoppedDirection = rb.velocity.normalized;
                    rb.velocity = Vector3.zero;
                } else if (Math.Abs(angle) < 70 && isStop == true) {
                    //debug.text = debug.text + "\nSTART";
                    isStop = false;
                    rb.velocity = stoppedDirection * rb.velocity.magnitude;
                } else {
                    isStop = false;
                    // turn and save new velocity
                    debug.text = debug.text + "\nRUNNING";
                    Quaternion rotation = Quaternion.Euler(0f, (-angle)*smooth, 0f);
                    rb.velocity = rotation * rb.velocity;
                    // reduce velocity ???
                    direction = rb.velocity.normalized;
                }

            }else{
                // apply previous direction: with this implementation we can't deal with jumps, we will keep jumping
                if (isStop == false) {
                    rb.velocity = direction * rb.velocity.magnitude;
                } else {
                    debug.text = debug.text + "\nSTOP";
                    rb.velocity = Vector3.zero;
                }
            }

            // rotate the player into velocity direction 
            if (isStop == false ) transform.Rotate(0, (-angle)*smooth*0.3f, 0, Space.Self); // TODO: use a method to look at the velocity direction instead???            
            // Sometimes in big change of direction the orientation can be lost ! 

            debug.text = debug.text + "\nRotation Direction: " + transform.eulerAngles;
            debug.text = debug.text + "\nVelocity Direction: " + rb.velocity.normalized;  
            debug.text = debug.text + "\nVelocity Magnitude:" + rb.velocity.magnitude;
            
        }else{
            debug.text = "CALIBRATION\nPress A button to calibrate your controllers";
            return;
        }

    }

}
