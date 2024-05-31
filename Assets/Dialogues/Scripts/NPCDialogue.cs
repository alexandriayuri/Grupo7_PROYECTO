using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public AdvancedDialogueSO[] conversation;

    private Transform player;
    private SpriteRenderer indicator;

    //private bool facingPlayer = false;

    private AdvancedDialogueManager advancedDialogueManager;
    public CinematicEffect cinematicEffect;

    //private bool playerNearby = false;
    private bool dialogueInitiated;
    // Start is called before the first frame update
    void Start()
    {
        advancedDialogueManager = GameObject.Find("DialogueManager").GetComponent<AdvancedDialogueManager>();
        indicator = GetComponent<SpriteRenderer>();
        indicator.enabled = false;
    }

    void Update()
    {
        if (dialogueInitiated && (Input.GetKeyDown(KeyCode.F)))
        {
            cinematicEffect.TriggerCinematic(true);
            indicator.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dialogueInitiated)
        {
            indicator.enabled = true;
            player = collision.transform;
            /*
            if (!facingPlayer)
            {
                // Check if the NPC is facing the player
                if ((player.position.x < transform.position.x && transform.parent.localScale.x > 0) ||
                    (player.position.x > transform.position.x && transform.parent.localScale.x < 0))
                {
                    Flip();
                    facingPlayer = true;
                }
            }
            */
            advancedDialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            indicator.enabled = false;
            //facingPlayer = false;

            advancedDialogueManager.TurnOffDialogue();
            dialogueInitiated= false;
            cinematicEffect.TriggerCinematic(false);
        }
    }

    /*
    private void Flip()
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }*/
}
