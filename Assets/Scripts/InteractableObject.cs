using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableObject : MonoBehaviour
{
    private SpriteRenderer indicator;
    private bool playerInRange;

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
            playerInRange = true;
            indicator.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            indicator.enabled = false;
        }
    }

    public void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
        }
    }

}
