using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Animator animator;
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
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
