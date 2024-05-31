using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    //private List<Button> menuButtons = new List<Button>();

    private GameObject mainMenu;
    private GameObject loadingScreen;

    private Button playButton;
    private Button continueButton;
    private Button settingsButton;
    private Button quitButton;
    private Button backButton;
    private Button yesButton;
    private Button noButton;

    private VisualElement mainMenuPanel;
    private VisualElement startGamePanel;
    private VisualElement continueGamePanel;
    private VisualElement settingsPanel;
    private VisualElement quitGamePanel;
    private VisualElement noDataPanel;

    private AudioSource audioSource;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        mainMenu = GameObject.Find("MainMenu");
        loadingScreen = GameObject.Find("LoadingScreen");
        loadingScreen.SetActive(false);

        // Reference the Panel
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Audio
        audioSource = GetComponent<AudioSource>();

        // Buttons
        playButton = root.Q("StartGameButton") as Button;
        continueButton = root.Q("ContinueGameButton") as Button;
        settingsButton = root.Q("SettingsButton") as Button;
        quitButton = root.Q("ExitButton") as Button;

        // Panels
        mainMenuPanel = root.Q<VisualElement>("MainMenuPanel");
        startGamePanel = root.Q<VisualElement>("StartGamePanel");
        continueGamePanel = root.Q<VisualElement>("ContinueGamePanel");
        settingsPanel = root.Q<VisualElement>("SettingsPanel");
        quitGamePanel = root.Q<VisualElement>("QuitGamePanel");
        noDataPanel = root.Q<VisualElement>("NoDataPanel");

        yesButton = root.Q("YesButton") as Button;
        noButton = root.Q("NoButton") as Button;
        backButton = root.Q("BackButton") as Button;

        RegisterCallbacks();
        
        ShowPanel(mainMenuPanel);
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        RegisterCallbacks();
    }

    private void RegisterCallbacks()
    {

        playButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        continueButton.RegisterCallback<ClickEvent>(OnContinueGameClick);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        quitButton.RegisterCallback<ClickEvent>(OnQuitGameClick);
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
        yesButton.RegisterCallback<ClickEvent>(OnYesButtonClick);
        noButton.RegisterCallback<ClickEvent>(OnNoButtonClick);

        var menuButtons = new List<Button> { playButton, continueButton, settingsButton, quitButton, backButton, yesButton, noButton };
        foreach (var button in menuButtons)
        {
            button.RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }
    /*
    private void OnDisable()
    {
        Debug.Log("OnDisable called.");
        UnregisterCallbacks();
    }

    private void UnregisterCallbacks()
    {
        playButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        continueButton.UnregisterCallback<ClickEvent>(OnContinueGameClick);
        settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitGameClick);
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClick);
        yesButton.UnregisterCallback<ClickEvent>(OnYesButtonClick);
        noButton.UnregisterCallback<ClickEvent>(OnNoButtonClick);

        var menuButtons = new List<Button> { playButton, continueButton, settingsButton, quitButton, backButton, yesButton, noButton };
        foreach (var button in menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }*/

    private void ShowPanel(VisualElement panelToShow)
    {
        // Hide all panels
        mainMenuPanel.style.display = DisplayStyle.None;
        startGamePanel.style.display = DisplayStyle.None;
        continueGamePanel.style.display = DisplayStyle.None;
        settingsPanel.style.display = DisplayStyle.None;
        quitGamePanel.style.display = DisplayStyle.None;

        // Show the specified panel
        panelToShow.style.display = DisplayStyle.Flex;
        Debug.Log("Showing panel: " + panelToShow.name);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Play button clicked.");
        ShowPanel(startGamePanel);

    }
    private void OnContinueGameClick(ClickEvent evt)
    {
        Debug.Log("Continue button clicked.");
        ShowPanel(continueGamePanel);

    }
    private void OnSettingsButtonClick(ClickEvent evt)
    {
        Debug.Log("Settings button clicked.");
        ShowPanel(settingsPanel);

    }
    private void OnQuitGameClick(ClickEvent evt)
    {
        Debug.Log("Quit button clicked.");
        ShowPanel(quitGamePanel);
    }

    private void OnYesButtonClick(ClickEvent evt)
    {
        Debug.Log("OnYesButtonClick called");
        if (startGamePanel.style.display == DisplayStyle.Flex)
        {

            Debug.Log("Loading new game level: " + _newGameLevel);
            mainMenu.SetActive(false);
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(_newGameLevel);
            //loadingScreen.SetActive(false);

        }

        else if (continueGamePanel.style.display == DisplayStyle.Flex)
        {
            if (PlayerPrefs.HasKey("SavedLevel"))
            {
                levelToLoad = PlayerPrefs.GetString("SavedLevel");
                Debug.Log("Loading saved game level: " + levelToLoad);
                SceneManager.LoadScene(levelToLoad);
            }

            else
            {
                Debug.LogWarning("No saved game found");
                ShowPanel(noDataPanel);
            }
        }

        else if (quitGamePanel.style.display == DisplayStyle.Flex)
        {
            Debug.Log("Quitting the game");
            Application.Quit();
        }
    }
    private void OnNoButtonClick(ClickEvent evt)
    {
        Debug.Log("No button clicked.");
        ShowPanel(mainMenuPanel);
    }

    private void OnBackButtonClick(ClickEvent evt)
    {
        Debug.Log("Back button clicked.");
        ShowPanel(mainMenuPanel);
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        Debug.Log("Button clicked: " + evt.target);
        audioSource.Play();
    }
}
