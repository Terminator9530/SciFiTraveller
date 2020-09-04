﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace since references UI Buttons directly
using UnityEngine.EventSystems; // include EventSystems namespace so can set initial input for controller support
using UnityEngine.SceneManagement; // include so we can load new scenes

public class MainMenuManager : MonoBehaviour {

	public int startLives=3; // how many lives to start the game with on New Game

	public GameObject SettingsPanel;
	public GameObject PauseMenuPanel;
	public GameObject PrivacyPanel;
	public GameObject ControlsPanel;
	public GameObject MainMenuPanel;
	public Slider slider;

	// references to Submenus
	public GameObject _MainMenu;
	public GameObject _LevelsMenu;
	public GameObject _AboutMenu;

	// references to Button GameObjects
	public GameObject MenuDefaultButton;
	public GameObject AboutDefaultButton;
	public GameObject LevelSelectDefaultButton;
	public GameObject QuitButton;

	// list the level names
	public string[] LevelNames;

	// reference to the LevelsPanel gameObject where the buttons should be childed
	public GameObject LevelsPanel;

	// reference to the default Level Button template
	public GameObject LevelButtonPrefab;
	
	// reference the titleText so we can change it dynamically
	public Text titleText;

	// store the initial title so we can set it back
	private string _mainTitle;

	private void Start()
	{
		if (PlayerPrefs.HasKey("Volume"))
		{
			slider.value = PlayerPrefs.GetFloat("Volume");
		}
		else
		{
			slider.value = 0.4f;
			AdjustVolume(0.4f);
		}
	}

	// init the menu
	void Awake()
	{
		// store the initial title so we can set it back
		_mainTitle = titleText.text;

		// disable/enable Level buttons based on player progress
		setLevelSelect();

		// Show the proper menu
		ShowMenu("MainMenu");
	}

	// loop through all the LevelButtons and set them to interactable 
	// based on if PlayerPref key is set for the level.
	void setLevelSelect() {
		// turn on levels menu while setting it up so no null refs
		_LevelsMenu.SetActive(true);

		// loop through each levelName defined in the editor
		for(int i=0;i<LevelNames.Length;i++) {
			// get the level name
			string levelname = LevelNames[i];

			// dynamically create a button from the template
			GameObject levelButton = Instantiate(LevelButtonPrefab,Vector3.zero,Quaternion.identity) as GameObject;

			// name the game object
			levelButton.name = levelname+" Button";

			// set the parent of the button as the LevelsPanel so it will be dynamically arrange based on the defined layout
			levelButton.transform.SetParent(LevelsPanel.transform,false);

			// get the Button script attached to the button
			Button levelButtonScript = levelButton.GetComponent<Button>();

			// setup the listener to loadlevel when clicked
			levelButtonScript.onClick.RemoveAllListeners();
			levelButtonScript.onClick.AddListener(() => loadLevel(levelname));

			// set the label of the button
			Text levelButtonLabel = levelButton.GetComponentInChildren<Text>();
			levelButtonLabel.text = levelname;

			// determine if the button should be interactable based on if the level is unlocked
			if (PlayerPrefManager.LevelIsUnlocked (levelname)) {
				levelButtonScript.interactable = true;
			} else {
				levelButtonScript.interactable = false;
			}
		}
	}

	// Public functions below that are available via the UI Event Triggers, such as on Buttons.

	// Show the proper menu
	public void ShowMenu(string name)
	{
		// turn all menus off
		_MainMenu.SetActive (false);
		_AboutMenu.SetActive(false);
		_LevelsMenu.SetActive(false);

		// turn on desired menu and set default selected button for controller input
		switch(name) {
		case "MainMenu":
			_MainMenu.SetActive (true);
			/*EventSystem.current.SetSelectedGameObject (MenuDefaultButton);*/
			titleText.text = _mainTitle;
			break;
		case "LevelSelect":
			_LevelsMenu.SetActive(true);
			/*EventSystem.current.SetSelectedGameObject (LevelSelectDefaultButton);*/
			titleText.text = "Level Select";
			break;
		case "About":
			_AboutMenu.SetActive(true);
			/*EventSystem.current.SetSelectedGameObject (AboutDefaultButton);*/
			titleText.text = "About";
			break;
		}
	}

	// load the specified Unity level
	public void loadLevel(string levelToLoad)
	{
		// start new game so initialize player state
		PlayerPrefManager.ResetPlayerState(startLives,false);

		// load the specified level
		SceneManager.LoadScene(levelToLoad);
	}

	// quit the game
	public void QuitGame()
	{
		Application.Quit ();
	}

	public void MainMenuMethod()
	{
		MainMenuPanel.SetActive(true);
		_AboutMenu.SetActive(false);
		SettingsPanel.SetActive(false);
		PrivacyPanel.SetActive(false);
		ControlsPanel.SetActive(false);
	}

	public void AboutMethod()
	{
		MainMenuPanel.SetActive(false);
		_AboutMenu.SetActive(true);
	}

	public void SettingsMethod()
	{
		if (MainMenuPanel != null)
			MainMenuPanel.SetActive(false);
		else
			PauseMenuPanel.SetActive(false);
		SettingsPanel.SetActive(true);
	}

	public void PrivacyMethod()
	{
		MainMenuPanel.SetActive(false);
		PrivacyPanel.SetActive(true);
	}

	public void ControlMethod()
	{
		MainMenuPanel.SetActive(false);
		ControlsPanel.SetActive(true);
	}

	public void OpenGitHub(string name)
	{
		Application.OpenURL("https://github.com/" + name);
	}

	public void OpenTwitter(string name)
	{
		Application.OpenURL("https://twitter.com/" + name);
	}

	public void OpenLinkedIn(string name)
	{
		Application.OpenURL("https://www.linkedin.com/in/" + name);
	}

	public void OpenFacebook(string name)
	{
		Application.OpenURL("https://www.facebook.com/" + name);
	}

	public void AdjustVolume(float vol)
	{
		PlayerPrefs.SetFloat("Volume", vol);
	}

	public void PauseMenuMethod()
	{
		PauseMenuPanel.SetActive(true);
		SettingsPanel.SetActive(false);
	}
}
