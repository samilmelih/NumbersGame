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
	Composer composer;

	float cooldownTime = 0.5f;
	float cooldown = 0.5f;
	bool isPlaying;

	void Start()
	{
		Instance = this;
		
		audioSource = GetComponent<AudioSource>();

		InitializeCardNotes();
	}

	void InitializeCardNotes()
	{
		cardNotes = Resources.LoadAll<AudioClip>("Music/CardNotes");
		composer = new Composer(cardNotes.Length);

		foreach(AudioClip ac in cardNotes)
			composer.AddNote(ac.name);
	}

	void Update()
	{
		if(isPlaying == true)
		{
			if(cooldown <= 0f)
			{
				cooldown = cooldownTime;
				isPlaying = false;
			}
			else
				cooldown -= Time.deltaTime;
		}	
	}

	public void PlayCardNote(Card card)
	{
		if(isPlaying == false)
		{
			int nextNote = composer.NextNote();
			audioSource.PlayOneShot(cardNotes[nextNote], DataTransfer.volume);

			isPlaying = true;
		}
	}
}
