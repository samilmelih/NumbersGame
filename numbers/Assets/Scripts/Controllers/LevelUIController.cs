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

	public void ShowAllCards()
	{
		GameController.Instance.ShowAllCards();
	}

	public void HowToPlay()
	{
		// TODO:
	}

	public void ShowMenuAnim()
	{
		GameController.Instance.levelPaused = true;
		optionAnimator.SetBool("open", true);
	}

	public void CloseMenuAnim()
	{
		GameController.Instance.levelPaused = false;
		optionAnimator.SetBool("open", false);
	}

	public void QuitGame()
	{
		// MainMenu Scene
		SceneManager.LoadScene(0);
	}
}
