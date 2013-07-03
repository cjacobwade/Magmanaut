using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
	
	public AudioClip[] soundFX;
	public AudioClip[] ambient;
	public AudioClip[] music;
	public AudioSource soundFXSource;
	public AudioSource ambientSource;
	public AudioSource musicSource;
	
	// Use this for initialization
	void Awake () 
	{
		PlaySound(1,0,100,true);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void PlaySound(int source,int index, int volume, bool loop)
	{
		switch(source)
		{
			case 1:
			{
				ambientSource.clip = ambient[index];
				ambientSource.volume = volume;
				ambientSource.Play();
				ambientSource.loop = loop;
				break;
			}
			
			case 2:
			{
				musicSource.clip = music[index];
				musicSource.volume = volume;
				musicSource.Play();
				musicSource.loop = loop;
				break;
			}
			
			default:
			{
				soundFXSource.clip = soundFX[index];
				soundFXSource.volume = volume;
				soundFXSource.Play();
				soundFXSource.loop = loop;
				break;
			}
		};

	}
	
	public void PlaySound(int source, int index, bool loop)
	{
		switch(source)
		{
			case 1:
			{
				ambientSource.clip = ambient[index];
				ambientSource.Play();
				ambientSource.loop = loop;
				break;
			}
			
			case 2:
			{
				musicSource.clip = music[index];
				musicSource.Play();
				musicSource.loop = loop;
				break;
			}
			
			default:
			{
				soundFXSource.clip = soundFX[index];
				soundFXSource.Play();
				soundFXSource.loop = loop;
				break;
			}
		};
	}
}
