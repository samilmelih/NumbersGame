using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Button btnSliderOn;
    public Button btnSliderOff;
    public Slider slider;
	const float mutedVolume = 0f;
	const float defaultVolume = 1f;
	float oldVolume = 0;

    // Use this for initialization
    private void Start()
    {
		slider.value = DataTransfer.volume;
    }
    
    public void SetVolume(float value)
    {
		DataTransfer.volume = value;
       
        if(value == slider.minValue)
        {
            btnSliderOn.gameObject.SetActive(false);
            btnSliderOff.gameObject.SetActive(true);
        }
        else
        {
            btnSliderOn.gameObject.SetActive(true);
        }

        if (oldVolume == 0 && value > 0)
        {
            btnSliderOff.gameObject.SetActive(false);
        }
        //else
        //{
        //   
        //}
        oldVolume = value;
    }

    public void MuteVolume(bool mute)
    {
        if (mute)
		{
            slider.value = mutedVolume;
			DataTransfer.volume = mutedVolume;
		}
        else
        {
			slider.value = defaultVolume;
			DataTransfer.volume = defaultVolume;
        }
    }

	public void MuteSFX(bool mute)
	{
		MenuController.Instance.MakeButtonSound();

		if (mute)
			DataTransfer.sfxVolume = mutedVolume;
		else
			DataTransfer.sfxVolume = defaultVolume;
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
