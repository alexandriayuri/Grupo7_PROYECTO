using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AdvancedDialogueManager : MonoBehaviour
{
    // The current NPC dialogue 
    private AdvancedDialogueSO currentConversation;
    private int stepNum;
    private bool dialogueActivated;

    // UI References
    private GameObject dialogueCanvas;
    private TMP_Text actor;
    private TMP_Text dialoguesText;

    private string currentSpeaker;

    public ActorSO[] actorSO;

    // Button References
    private GameObject[] optionButton;
    private TMP_Text[] optionButtonText;
    private GameObject optionsWindow;

    // GUI to deactivate
    private GameObject gui;

    // Player Freeze
    private PlayerController playerController;
    private PlayerInput playerInput;

    // Typewriter effect
    [SerializeField]
    private float typingSpeed = 0.02f;
    private Coroutine typeWriterRoutine;
    private bool canContinueText = true;

    void Start()
    {
        gui = GameObject.Find("bookIcon_GUI");

        // Find player controller script
        playerController = GameObject.Find("Player(Haki)").GetComponent<PlayerController>();
        playerInput = GameObject.Find("Player(Haki)").GetComponent<PlayerInput>();

        // Find Buttons
        optionButton = GameObject.FindGameObjectsWithTag("OptionButton");
        optionsWindow = GameObject.Find("OptionsWindow");
        optionsWindow.SetActive(false);

        // Find the TMP Text on the buttons
        optionButtonText = new TMP_Text[optionButton.Length];
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButtonText[i] = optionButton[i].GetComponentInChildren<TMP_Text>();
        }

        // Turn off the buttons at the start
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(false);
        }

        dialogueCanvas = GameObject.Find("DialogueCanvas");
        actor = GameObject.Find("ActorText").GetComponent<TMP_Text>();
        dialoguesText = GameObject.Find("DialoguesText").GetComponent<TMP_Text>();

        dialogueCanvas.SetActive(false);

    }

    void Update()
    {
        if (dialogueActivated && canContinueText && ((Input.GetKeyDown(KeyCode.F)) || (Input.GetMouseButtonDown(0))))
        {
            // Deactivate GUI
            gui.SetActive(false);

            // Freeze the player
            playerController.enabled = false;
            playerInput.enabled = false;


            // Cancel dialogue if there are no lines of dialogue remaining
            if (stepNum >= currentConversation.actors.Length)
                TurnOffDialogue();

            // Continue dialogue
            else
                PlayDialogue();
        }
    }

    void PlayDialogue()
    {
        // If it's a random NPC
        if (currentConversation.actors[stepNum] == DialogueActors.Random)
            SetActorInfo(false);

        // If it's a recurring character
        else
            SetActorInfo(true);

        // Display Dialogue
        actor.text = currentSpeaker;

        // If there is a branch...
        if (currentConversation.actors[stepNum] == DialogueActors.Branch)
        {
            for (int i = 0; i < currentConversation.optionText.Length; i++)
            {
                if (currentConversation.optionText[i] == null)
                {
                    optionButton[i].SetActive(false);
                }
                else
                {
                    optionButtonText[i].text = currentConversation.optionText[i];
                    optionButton[i].SetActive(true);
                }

                // Set the first button to be auto-selected
                optionButton[0].GetComponent<Button>().Select();
            }
        }

        // Keep routine from running multiple times at the same time 
        if (typeWriterRoutine != null)
            StopCoroutine(typeWriterRoutine);

        if (stepNum < currentConversation.dialogue.Length)
            typeWriterRoutine = StartCoroutine(TypeWriterEffect(dialoguesText.text = currentConversation.dialogue[stepNum]));

        else
            optionsWindow.SetActive(true);

        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    void SetActorInfo(bool recurringCharacter)
    {
        if (recurringCharacter)
        {
            for (int i = 0; i < actorSO.Length; i++)
            {
                if (actorSO[i].name == currentConversation.actors[stepNum].ToString())
                    currentSpeaker = actorSO[i].actorName;
            }
        }

        else
            currentSpeaker = currentConversation.randomActorName;
    }

    public void Option(int optionNum)
    {
        foreach (GameObject button in optionButton)
            button.SetActive(false);

        if (optionNum == 0)
            currentConversation = currentConversation.option0;

        if (optionNum == 1)
            currentConversation = currentConversation.option1;

        stepNum = 0;
    }

    private IEnumerator TypeWriterEffect(string line)
    {
        dialoguesText.text = "";
        canContinueText = false;
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                dialoguesText.text = line;
                break;
            }
            dialoguesText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueText = true;
    }

    public void InitiateDialogue(NPCDialogue npcDialogue)
    {
        // current array
        currentConversation = npcDialogue.conversation[0];

        dialogueActivated = true;
    }

    public void TurnOffDialogue()
    {
        stepNum = 0;

        dialogueActivated = false;
        if (optionsWindow != null)
        {
            optionsWindow.SetActive(false);
        }
        
        if (dialogueCanvas !=null)
        {
            dialogueCanvas.SetActive(false);
        }

        // Activate GUI
        if (gui != null)
        {
            gui.SetActive(true);
        }

        // Unfreeze player if the playerController and playerInput are still valid
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
    }
}

public enum DialogueActors
{
    Haki,
    Paqui,
    Random,
    Branch
};