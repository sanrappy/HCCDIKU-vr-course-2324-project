using UnityEngine;
using TMPro;

public class PointsGame : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfFlags;
    public int scorePlayer;
    public TextMeshPro debug;

    void Start(){
        scorePlayer = 0;
    }

    public void AddPoint()
    {
        scorePlayer++;
    }

    void Update() {
        if (debug) debug.text = debug.text + "\nScore: " + scorePlayer.ToString();
    }
}
