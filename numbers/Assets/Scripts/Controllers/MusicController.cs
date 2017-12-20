using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
	[HideInInspector]
	public static MusicController Instance;

	AudioSource audioSource;
	AudioClip[] cardTones;

	int toneCount;

	void Start()
	{
		if(Instance == null)
			Instance = this;
		
		audioSource = GetComponent<AudioSource>();

		AudioClip[] tones = Resources.LoadAll<AudioClip>("Music/CardTones/midi_wood_block");

		toneCount = tones.Length;
		cardTones = new AudioClip[toneCount + 1];

		for(int i = 0; i < toneCount; i++)
		{
			int no = int.Parse(tones[i].name);
			cardTones[no] = tones[i];
		}
	}

	public void PlayCardTone(Card card)
	{
		// FIXME: This calculation should be changed after level designs changed.
		int totalCardCount = LevelController.Instance.currLevel.totalCardCount;
		int cardNumber = card.cardNumber;

		// totalCardCount = 4
		// totalCardCount / 2 = 2
		// (toneCount / 2) - (totalCardCount / 2) + cardNumber

		int clipNumber = (toneCount / 2) - (totalCardCount / 2) + cardNumber;
		clipNumber = Mathf.Clamp(clipNumber, 1, toneCount);

		audioSource.PlayOneShot(cardTones[clipNumber], GameController.volume);
	}
}
