using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class TargetClass : MonoBehaviour
{
    public Color color;

    public GameObject player;

    public GameObject target;

    void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Player") {

            //ContactPoint contact = collision.contacts[0];
            this.gameObject.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.red);
            this.gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", Color.red);
            this.gameObject.GetComponent<Renderer>().materials[2].SetColor("_Color", Color.red);

            target.SetActive(true);
            //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            //Vector3 position = contact.point;
            //Instantiate(explosionPrefab, position, rotation);
            //Destroy(gameObject);
        }
    }

}
