using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public static MenuController Instance;

	// We will forget these indexes later.
	// A better solution would be nice. Maybe enum.
	// 0 - Main Menu Panel
	// 1 - Select Mode Panel
	// 2 - Level Picker Panel
	// 3 - Settings Panel
	public GameObject[] panels;

	public GameObject developerToolsButton;

	AudioSource audioSource;
	public AudioClip buttonClickedSound;
    SettingsController settingsController;
	void Start()
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
        FindObjectOfType<SettingsController>().OnLanguageChange += SetLanguage;
		// LevelPickerController script must be executed before
		// MenuController. I have set it up by script execution order.
		// This is just a note for future.
		if(DataTransfer.currOpenPanel == 2)
			SelectMode((int) DataTransfer.levelMode);


        //TODO: read from playerprefs and set it
        if (PlayerPrefs.HasKey("lang") == true)
        {
            SetLanguage(PlayerPrefs.GetInt("lang"));
        }
        else
        {
            SetLanguage(0);
        }

	#if UNITY_EDITOR
		developerToolsButton.SetActive(true);
	#endif
	}

	public void OpenPanel(int panelIndex)
	{
		for(int i = 0; i < panels.Length; i++)
		{
			if(i == panelIndex)
				panels[i].SetActive(true);
			else
				panels[i].SetActive(false);
		}
	}

	public void SelectMode(int mode)
	{
		// Select level
		DataTransfer.levelMode = (LevelMode) mode;

		// Open Level Picker Panel
		OpenPanel(2);
		LevelPickerController.Instance.LoadLevels();
	}

	public void AreYouSure(bool opt)
	{
		if(opt == true)
			Application.Quit();
		else
			OpenPanel(0);
	}
		
    public void Button_Exit()
    {
		OpenPanel(4);
    }

	public void MakeButtonSound()
	{
		audioSource.PlayOneShot(buttonClickedSound, DataTransfer.sfxVolume);
	}

	#if UNITY_EDITOR
	public void OpenDeveloperTools()
	{
		// Load LevelGenerator Scene
		SceneManager.LoadScene(2);
	}
	#endif
    public void SetLanguage(int index)
    {
        ///Main Menu
        Button[] buttons=panels[0].GetComponentsInChildren<Button>();
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.playButton[index];
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.settingsButton[index];
        buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.exitButton[index];

       // panels[0].transform.Find("Title").GetComponent<TextMeshProUGUI>().text = StringLiterals.gameNameText[index];

        ///Select mode Menu
        buttons = panels[1].transform.Find("Content").GetComponentsInChildren<Button>();
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.classicButton[index];
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.dontForgetButton[index];
        buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.noMistakeButton[index];

        //Settings Menu
        buttons = panels[3].transform.Find("Buttons").GetComponentsInChildren<Button>();
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.creditsButton[index];
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.resetProgressButton[index];
        
        TextMeshProUGUI[] textMeshProUGUI= panels[3].GetComponentsInChildren<TextMeshProUGUI>();
        textMeshProUGUI[0].text = StringLiterals.settingsText[index];
        textMeshProUGUI[1].text = StringLiterals.musicText[index];
        textMeshProUGUI[2].text = StringLiterals.sfxText[index];
        textMeshProUGUI[3].text = StringLiterals.langText[index];

        textMeshProUGUI = panels[3].transform.Find("Credits").GetComponentsInChildren<TextMeshProUGUI>();
        textMeshProUGUI[0].text = StringLiterals.creditsButton[index];
        textMeshProUGUI[1].text = StringLiterals.creditsText[index];

        //Exit menu
        buttons = panels[4].GetComponentsInChildren<Button>();
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.yesButton[index];
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.noButton[index];

        panels[4].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.exitQuestionText[index];


        PlayerPrefs.SetInt("lang", index);


    }
}
