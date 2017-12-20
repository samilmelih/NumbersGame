using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider slider;
	const float mutedVolume = 0f;
	const float defaultVolume = 1f;

    // Use this for initialization
    private void Start()
    {
		slider.value = GameController.volume;
    }

    public void SetVolume(float value)
    {
		GameController.volume = value;
    }

    public void MuteVolume(bool mute)
    {
        if (mute)
		{
            slider.value = mutedVolume;
			GameController.volume = mutedVolume;
		}
        else
        {
			slider.value = defaultVolume;
			GameController.volume = defaultVolume;
        }
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
