using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private AudioSource[] audioSources;

	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	public void Awake()
	{
		audioSources = GetComponentsInChildren<AudioSource>();
	}

	public void Play(AudioClip sound, bool randomize)
	{
		for(int i = 0; i < audioSources.Length; i++)
		{
			if(audioSources[i].clip == sound)
			{
				if (randomize)
				{
					float randomPitch = Random.Range(lowPitchRange, highPitchRange);

					audioSources[i].pitch = randomPitch;
				}

				audioSources[i].Stop();
				audioSources[i].Play();
			}
		}
	}
}
