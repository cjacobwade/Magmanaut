using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
	
	public AudioClip[] soundFX;
	public AudioClip[] music;
	public AudioSource soundFXSource;
	public AudioSource musicSource;
	
	public enum soundTypes
	{
		soundFX,
		music,
	};
	
	// Use this for initialization
	void Awake () 
	{
		PlaySound(0,0,100,true);
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
