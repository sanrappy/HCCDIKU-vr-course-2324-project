using System;
using UnityEngine;
using TMPro;

public class TurningControllers : MonoBehaviour
{

    public Transform leftController;
    public Transform rightController;
    public TextMeshPro debug;
    public float smooth; // Map controller rotation angle to the player direction and orientation
    public int minRotation; // Reduce Noise to start turning
    public float maxRotation; // Control when to break
    public int userId;
    public int trialId;
    public float timer;

    private Rigidbody rb;
    private float angle;
    private bool isStop = false; // Implement the STOP mechanism
    private bool isCalibrate = false; // Calibrate the controllers rotation
    private float initialLeftControllerRotation; // Set by calibration
    private float initialRightControllerRotation; // Set by calibration
    private Quaternion direction;
    private Vector3 stoppedDirection;
    private float prevAngle;
    private bool isRecording;
    private Vector3 checkPoint;
    private Vector3 checkPointVelocity;
    private int callToCheckPoint;

    // Start is called before the first frame update
    void  Start()
    {   
        minRotation = Math.Max(15,minRotation);
        isStop = false;
        isCalibrate = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        prevAngle = 0;
        isRecording = false;
        timer = 0.0f;
        callToCheckPoint = 0;
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
            isRecording = true;
            checkPoint = this.transform.position;
            checkPointVelocity = rb.velocity;
        }

        if (isCalibrate == true) {

            if (OVRInput.GetDown(OVRInput.Button.Two) && isRecording == true) {
                // restore position to last checkpoint
                this.gameObject.transform.position = new Vector3(checkPoint.x,checkPoint.y+7,checkPoint.z);
                rb.velocity = checkPointVelocity;
                callToCheckPoint++;
            }
            
            if (isRecording) timer += Time.deltaTime;
            if (debug) debug.text = "Press B button to restart from the last checkpoint\nTime: " + timer;

            float leftControllerRotation = leftController.eulerAngles.z - initialLeftControllerRotation;
            float rightControllerRotation = rightController.eulerAngles.z - initialRightControllerRotation;
            
            //if (debug) debug.text = "Left Controller Angle: " + leftControllerRotation + "\nRight Controller Angle: " + rightControllerRotation;

            if (leftControllerRotation < 180 && leftControllerRotation > minRotation) {
                
                // Get the new forward direction after rotation
                angle = leftControllerRotation;

                //if (debug) debug.text = debug.text + "\nLeft Turn Direction: " + angle;

            } else if (rightControllerRotation > -160 && Math.Abs(rightControllerRotation) > minRotation) {
                
                // Get the new forward direction after rotation
                angle = rightControllerRotation;

                //if (debug) debug.text = debug.text + "\nRight Turn Direction: " + angle;

            } else {
                angle = 0;
            }

            if (angle != 0) {
                if (Math.Abs(angle) > maxRotation) {
                    //if (debug) debug.text = debug.text + "\nSTOP";
                    isStop = true;
                    stoppedDirection = rb.velocity.normalized;
                    rb.velocity = Vector3.zero;
                } else if (isStop == true && angle * prevAngle < 0) {
                    // if I stop and change direction start again
                    isStop = false;
                    Quaternion rotation = Quaternion.Euler(0f, (-angle)*smooth, 0f);
                    rb.velocity = stoppedDirection * rb.velocity.magnitude;
                    rb.velocity = rotation * rb.velocity;
                } else if (isStop == true && angle * prevAngle > 0) {
                    // keep stopping
                    //if (debug) debug.text = debug.text + "\nSTOP";
                    rb.velocity = Vector3.zero;
                } else {
                    isStop = false;
                    // turn and save new velocity
                    Quaternion rotation = Quaternion.Euler(0f, (-angle)*smooth, 0f);
                    rb.velocity = rotation * rb.velocity;
                    // reduce velocity ???
                    direction = rotation;
                }
            }else{
                // apply previous direction: with this implementation we can't deal with jumps, we will keep jumping
                if (isStop == false) {
                    // apply the same rotation 
                    rb.velocity = direction * rb.velocity;
                } else {
                    //if (debug) debug.text = debug.text + "\nSTOP";
                    rb.velocity = Vector3.zero;
                }
            }
            // rotate the player into velocity direction 
            if (isStop == false ) {
                transform.Rotate(0, (-angle)*smooth*0.5f, 0, Space.Self); // TODO: use a method to look at the velocity direction instead???            
            }

            prevAngle = angle;

            //if (debug) debug.text = debug.text + "\nRotation Direction: " + transform.eulerAngles;
            //if (debug) debug.text = debug.text + "\nVelocity Direction: " + rb.velocity.normalized;  
            //if (debug) debug.text = debug.text + "\nVelocity Magnitude:" + rb.velocity.magnitude;
            
        }else{
            debug.text = "Welcome to Giant Slalom Skiing Simulator!\nTry to turn around every flag and reach the finish line!\nYou will test 6 different game version\nAt the end of each run you will be asked too give a feedback\nCALIBRATION\nPress A button to calibrate your controllers and start skiing!";
            return;
        }

    }

    public string[] GameStats () {
        isRecording = false;
        float score = this.gameObject.GetComponent<PointsGame>().scorePlayer;
        int totalScore = this.gameObject.GetComponent<PointsGame>().numberOfFlags;
        int accuracy = (int) (score / totalScore * 100);
        return new string[] {timer.ToString(), accuracy.ToString(), callToCheckPoint.ToString()};
    }

    public void UpdateCheckpoint (Vector3 position){
        checkPoint = position;
        checkPointVelocity = rb.velocity;
    }

}
