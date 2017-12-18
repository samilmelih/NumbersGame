using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetProgress()
	{
		ProgressController.ResetProgress();
	}

	public void ExitSettings()
	{
		// Return to Main Menu
		SceneManager.LoadScene(0);
	}
}
