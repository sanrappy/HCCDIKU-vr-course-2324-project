using UnityEngine;
using TMPro;

public class PointsGame : MonoBehaviour
{
    // Start is called before the first frame update
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
        debug.text = debug.text + "\n Score: " + scorePlayer.ToString();
    }
}
