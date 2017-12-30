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
	public Button btnSfxSliderOn;
	public Button btnSfxSliderOff;
    public Slider slider;
	public const float mutedVolume = 0f;
	public const float defaultVolume = 1f;
	float oldVolume = 0;

    private void OnEnable()
    {
		float volume = DataTransfer.volume;
		float sfxVolume = DataTransfer.sfxVolume;

		slider.value = DataTransfer.volume;
		if(volume == mutedVolume)
		{
			btnSliderOn.gameObject.SetActive(false);
			btnSliderOff.gameObject.SetActive(true);
		}
		else
		{
			btnSliderOn.gameObject.SetActive(true);
			btnSliderOff.gameObject.SetActive(false);
		}

		if(sfxVolume == mutedVolume)
		{
			btnSfxSliderOn.gameObject.SetActive(false);
			btnSfxSliderOff.gameObject.SetActive(true);
		}
		else
		{
			btnSfxSliderOn.gameObject.SetActive(true);
			btnSfxSliderOff.gameObject.SetActive(false);
		}

    }
    
    public void SetVolume(float value)
    {
		DataTransfer.volume = value;
		ProgressController.SetVolume(value);

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

		ProgressController.SetVolume(DataTransfer.volume);
    }

	public void MuteSFX(bool mute)
	{
		MenuController.Instance.MakeButtonSound();

		if (mute)
			DataTransfer.sfxVolume = mutedVolume;
		else
			DataTransfer.sfxVolume = defaultVolume;

		ProgressController.SetSfxVolume(DataTransfer.sfxVolume);
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
