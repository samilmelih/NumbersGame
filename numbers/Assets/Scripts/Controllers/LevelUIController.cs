using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
	public Animator optionAnimator;

	public void NextLevel()
	{
		GameController.Instance.ChangeSucceedScreenState();
		GameController.Instance.SetupLevel();
	}

	public void HowToPlay()
	{
		// TODO:
	}

	public void ShowMenuAnim()
	{
		optionAnimator.SetBool("open", true);
	}

	public void CloseMenuAnim()
	{
		optionAnimator.SetBool("open", false);
	}

	public void QuitGame()
	{
		// MainMenu Scene
		SceneManager.LoadScene(0);
	}
}
