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

	void Start()
	{
		Instance = this;

		// LevelPickerController script must be executed before
		// MenuController. I have set it up by script execution order.
		// This is just a note for future.
		if(DataTransfer.currOpenPanel == 2)
			SelectMode((int) DataTransfer.levelMode);
		

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
		
    public void ExitGame()
    {
        Application.Quit();
    }


	#if UNITY_EDITOR
	public void OpenDeveloperTools()
	{
		// Load LevelGenerator Scene
		SceneManager.LoadScene(2);
	}
	#endif
}
