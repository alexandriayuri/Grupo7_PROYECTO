using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Transform playerTransform;
    public int sceneBuildIndex;
    private bool playerInRange;

    public float xPosition;
    public float yPosition;

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void Update()
    {
        if (playerInRange)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            playerTransform.position = new Vector3(xPosition, yPosition, 0);
        }
    }
}
