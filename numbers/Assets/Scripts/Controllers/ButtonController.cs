using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Animator animator;
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
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
		// MainMenu Scene
        SceneManager.LoadScene(0);
    }

	public void OpenDeveloperTools()
	{
		// LevelGenerator Scene
		SceneManager.LoadScene(3);
	}

    public void ShowMenuAnim()
    {
        animator.SetBool("open", true);
    }

    public void CloseMenuAnim()
    {
        animator.SetBool("open", false);
    }

    public void NextLevel()
    {
        // I guess, they should be a method in GameController.
        GameController.Instance.ChangeSucceedScreenState();
        GameController.Instance.SetupLevel();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
