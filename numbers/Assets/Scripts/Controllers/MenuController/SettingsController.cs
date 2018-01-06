﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject CreditsScreen;
	public const float mutedVolume = 0f;
	public const float defaultVolume = 1f;
	float oldVolume = 0;

	public Image[] languageImages;

    public delegate void ChangeLanguageHandler(int index);
    public event ChangeLanguageHandler OnLanguageChange;

    private void OnEnable()
    {
		float volume = DataTransfer.volume;
		float sfxVolume = DataTransfer.sfxVolume;

		SetLanguage((int) PlayerPrefs.GetInt("lang"));

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

    public void CreditsOn()
    {
        CreditsScreen.SetActive(true);
    }

    public void CreditsOFF()
    {
        CreditsScreen.SetActive(false);
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

    public void SetLanguage(int index)
    {
        DataTransfer.language = (Language)index;

		Color pressed = languageImages[index].color;
		Color unpressed = languageImages[(index + 1) % 2].color;

		pressed.a = 1f;
		unpressed.a = 0.6f;

		languageImages[index].color = pressed;
		languageImages[(index + 1) % 2].color = unpressed;

        if(OnLanguageChange != null)
        {
            OnLanguageChange(index);
        }
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
		SetLanguage((int) Language.ENGLISH);
	}

	public void ExitSettings()
	{
		// Return to Main Menu
		SceneManager.LoadScene(0);
	}
}
