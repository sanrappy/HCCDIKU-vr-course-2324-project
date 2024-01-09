using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;
using TMPro;
using System.Data.Common;

public class TargetTable : MonoBehaviour
{

    private int scoreval = 0;
    
    private int mode = 1; // 0: easy, 1: medium, 2: difficult, 3: impossible

    public TextMeshPro scoreUI;
    public TextMeshPro modeUI;
    
    void Update() 
    {
        scoreUI.text = "Score: " + scoreval.ToString();
        
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {   
            // increase mode
            mode++;
            if (mode > 3) mode = 0;
            //this.gameObject.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.red);
            UpdateScale(mode);
        } 
        
    }

    void UpdateScale(int mode)
    {
        switch (mode)
        {
            case 0:
                this.GetComponent<Renderer>().enabled = true;
                this.GetComponent<Transform>().localScale = new Vector3(2.2f, 0.05f, 2.2f);
                this.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.green);
                modeUI.text = "Mode: Easy";
                break;
            case 1:
                this.GetComponent<Transform>().localScale = new Vector3(1.5f, 0.05f, 1.5f);
                this.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.blue );  
                modeUI.text = "Mode: Medium";
                break;
            case 2:
                this.GetComponent<Transform>().localScale = new Vector3(0.8f, 0.05f, 0.8f);
                this.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.red);
                modeUI.text = "Mode: Difficult";
                break;
            case 3:
                this.GetComponent<Transform>().localScale = new Vector3(0.8f, 0.05f, 0.8f);
                this.GetComponent<Renderer>().enabled = false;
                modeUI.text = "Mode: Hard";
                break;

        }
    }

    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject.tag == "ball") 
        {
            print("Target Cylinder Triggered ");
            scoreval++;
        }
    }

}
