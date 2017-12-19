using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public GameObject developerToolsButton;
	public GameObject mainMenuPanel;
	public GameObject selectModePanel;

	void Start()
	{
		if(GameController.mainMenuOpen == false)
			OpenModePicker();
		

	#if UNITY_EDITOR
		developerToolsButton.SetActive(true);
	#endif
	}

    public void OpenModePicker()
    {
		// Hide MainMenuPanel
		mainMenuPanel.SetActive(false);

		// Load SelectModePanel
		selectModePanel.SetActive(true);
    }

    public void OpenSettings()
    {
		// Load Settings Scene
        SceneManager.LoadScene(3);
    }

	public void OpenDeveloperTools()
	{
		// Load LevelGenerator Scene
		SceneManager.LoadScene(4);
	}

	public void SelectMode(int modeNo)
	{
		// Select level
		LevelManager.currLevelMode = (LevelMode) modeNo;

		// Load Level Scene
		SceneManager.LoadScene(1);
	}
		
	public void ReturnMainMenu()
	{
		// Hide MainMenuPanel
		mainMenuPanel.SetActive(true);

		// Load SelectModePanel
		selectModePanel.SetActive(false);
	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
