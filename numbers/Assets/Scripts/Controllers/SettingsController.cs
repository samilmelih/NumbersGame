using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {


    public AudioMixer audioMixer;
    public Slider slider;
    float mutedValue = -80f;
    float unMutedValue = -60f;
    // Use this for initialization
    private void Start()
    {
        float val;
        audioMixer.GetFloat("volume",out val);

        slider.value = val;
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("volume", value);
    }

    public void MuteVolume(bool mute)
    {
        if (mute)
        {
            audioMixer.SetFloat("volume", mutedValue);
            slider.value = mutedValue;
        }
        else
        {
            audioMixer.SetFloat("volume", unMutedValue);
            slider.value = unMutedValue;
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
