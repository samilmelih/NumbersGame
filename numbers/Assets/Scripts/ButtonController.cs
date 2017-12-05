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
        GameController.Instance.SetUpGame();
        GameController.Instance.ChangeSucceedScreenState();
        GameController.Instance.level++;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
