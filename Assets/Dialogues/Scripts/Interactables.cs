using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    private SpriteRenderer indicator;


    // Start is called before the first frame update
    void Start()
    {
        indicator = GetComponent<SpriteRenderer>();
        indicator.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            indicator.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            indicator.enabled = false;
        }
    }
}
