using UnityEngine;
using TMPro;

public class PointsGame : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfFlags;
    public int scorePlayer;
    public TextMeshPro debug;

    private bool isFinish;

    void Start(){
        isFinish = false;
        scorePlayer = 0;
    }

    public void AddPoint()
    {
        scorePlayer++;
    }

    public void Finish(){
        isFinish = true;
    }

    void Update() {
        if (isFinish == false) {
            if (debug) debug.text = debug.text + "\nScore: " + scorePlayer.ToString();
        }
    }
}
