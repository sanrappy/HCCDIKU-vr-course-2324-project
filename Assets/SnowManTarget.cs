using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SnowManTarget : MonoBehaviour
{
    //public Color color;

    public GameObject player;

    public GameObject _explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject == player) {

            // instace the prefab at our position and rotation
            Instantiate(_explosionPrefab, transform.position, transform.rotation);

            // then destroy the game object that this component is attached to
            Destroy(this.gameObject);
        }
    }
}
