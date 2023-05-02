using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D col;

   
    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
    }

    // Detect when object enters range
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Add(collider);
            FindObjectOfType<AudioManager>().Play("BigFoot");
        }


    }

    // Detect when object leaves range
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(collider);
            FindObjectOfType<AudioManager>().Stop("BigFoot");
        }
    }

}
