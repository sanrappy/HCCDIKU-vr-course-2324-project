using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class ResetScene : MonoBehaviour
{
 
    public TextMeshPro debug;
    public int nextConfig; // load the scene with the next configuration to test, -1 if is the last one

    private bool isFinish;
    private bool isLastConfig;

    // Start is called before the first frame update
    void Start()
    {
        isFinish = false;
        isLastConfig = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Three) && (isFinish == true)){
            Debug.Log("X button pressed");
            // Move to next scene
            if (nextConfig != -1) SceneManager.LoadScene(nextConfig);
            else isLastConfig = true;
        }

        if (isLastConfig == true) {
            debug.text = "Congratulations!\nYou completed the experiment\nThanks for your time!";
        }
    }

    public void setFinish() {
        isFinish = true;
    }

}
 