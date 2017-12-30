using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
	[HideInInspector]
	public static MusicController Instance;

	AudioSource audioSource;
	AudioClip[] cardNotes;
	AudioClip buttonClickedSound;
	Composer composer;

	const float maxCardVolume = 0.7f;

	void Start()
	{
		Instance = this;
		
		audioSource = GetComponent<AudioSource>();

		InitializeCardNotes();
	}

	void InitializeCardNotes()
	{
		buttonClickedSound = Resources.Load<AudioClip>("Music/button_clicked_sound");
		cardNotes = Resources.LoadAll<AudioClip>("Music/CardNotes");
		composer = new Composer(cardNotes.Length);

		foreach(AudioClip ac in cardNotes)
			composer.AddNote(ac.name);
	}
		
	public void PlayCardNote(Card card)
	{
		int nextNote = composer.NextNote();

		// It's clamped because card note sounds too high.
		// This is the max that we can give.
		float volume = Mathf.Clamp(DataTransfer.volume, 0f, maxCardVolume);
		audioSource.PlayOneShot(cardNotes[nextNote], volume);
	}

	public void MakeButtonSound()
	{
		audioSource.PlayOneShot(buttonClickedSound, DataTransfer.sfxVolume);
	}
}
