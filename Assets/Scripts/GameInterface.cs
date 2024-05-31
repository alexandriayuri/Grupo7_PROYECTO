using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameInterface : MonoBehaviour
{
    private GameObject gameInterface;
    private Button objectivesButton;
    private Button inventoryButton;
    private Button collectiblesButton;
    private Button achievementsButton;
    private Button exitButton;
    private Button diaryButton;

    //private List<Button> menuButtons = new List<Button>();
    private VisualElement bookPanel;

    private AudioSource audioSource;

    private void Awake()
    {
        gameInterface = GameObject.Find("GameInterface");

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        audioSource = GetComponent<AudioSource>();

        objectivesButton = root.Q("ObjectivesButton") as Button;
        objectivesButton.RegisterCallback<ClickEvent>(OnObjectivesButtonClick);

        inventoryButton = root.Q("InventoryButton") as Button;
        inventoryButton.RegisterCallback<ClickEvent>(OnInventoryButtonClick);

        collectiblesButton = root.Q("CollectiblesButton") as Button;
        collectiblesButton.RegisterCallback<ClickEvent>(OnCollectiblesButtonClick);

        achievementsButton = root.Q("AchievementsButton") as Button;
        achievementsButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClick);

        exitButton = root.Q("ExitButton") as Button;
        exitButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClick);

        diaryButton = root.Q("LibraryButton") as Button;
        diaryButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClick);

        bookPanel = root.Q<VisualElement>("BookPanel");
        
        RegisterCallbacks();

    }
    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        RegisterCallbacks();
    }

    private void RegisterCallbacks()
    {

        objectivesButton.RegisterCallback<ClickEvent>(OnObjectivesButtonClick);
        inventoryButton.RegisterCallback<ClickEvent>(OnInventoryButtonClick);
        collectiblesButton.RegisterCallback<ClickEvent>(OnCollectiblesButtonClick);
        achievementsButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClick);
        exitButton.RegisterCallback<ClickEvent>(OnExitButtonClick);
        diaryButton.RegisterCallback<ClickEvent>(OnDiaryButtonClick);

        var menuButtons = new List<Button> { objectivesButton, inventoryButton, collectiblesButton, achievementsButton, exitButton, diaryButton };
        foreach (var button in menuButtons)
        {
            button.RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnObjectivesButtonClick(ClickEvent evt)
    {

    }
    private void OnInventoryButtonClick(ClickEvent evt)
    {

    }
    private void OnCollectiblesButtonClick(ClickEvent evt)
    {

    }
    private void OnAchievementsButtonClick(ClickEvent evt)
    {

    }

    private void OnExitButtonClick(ClickEvent evt)
    {
        Debug.Log("Exit Button clicked");
        bookPanel.style.display = DisplayStyle.None;
        gameInterface.SetActive(false);

    }
    private void OnDiaryButtonClick(ClickEvent evt)
    {

    }
    private void OnAllButtonsClick(ClickEvent evt)
    {
        Debug.Log("Button clicked: " + evt.target);
        audioSource.Play();
    }
}
