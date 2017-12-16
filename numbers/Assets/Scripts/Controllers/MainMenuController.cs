using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public GameObject developerToolsButton;

	void Start()
	{
	#if UNITY_EDITOR
		developerToolsButton.SetActive(true);
	#endif
	}

    public void PlayButton()
    {
		// Level_1 Scene
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
		// SettingsWindow Scene
        SceneManager.LoadScene(3);
    }

	public void OpenDeveloperTools()
	{
		// LevelGenerator Scene
		SceneManager.LoadScene(4);
	}
		
    

    public void Exit()
    {
        Application.Quit();
    }
}
