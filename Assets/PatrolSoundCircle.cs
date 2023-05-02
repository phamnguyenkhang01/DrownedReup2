using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSoundCircle : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D col;
    // private bool played;


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
          /*  if (!played)
                FindObjectOfType<AudioManager>().Play("Ambient"); 
            played = true; */
            Invoke("Monster", 2f);

        }
    }
    void Monster()
    {
        FindObjectOfType<AudioManager>().Play("Monster");
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(collider);
            FindObjectOfType<AudioManager>().Stop("Monster");
        }
    }


}
