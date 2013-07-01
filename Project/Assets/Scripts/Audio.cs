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
		PlaySound(soundFX,soundFXSource,0,100,true);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void PlaySound(AudioClip[] library,AudioSource source,int index, int volume, bool loop)
	{
		source.clip = library[index];
		source.volume = volume;
		source.Play();
		source.loop = loop;
	}
	
	void PlaySound(AudioClip[] library,AudioSource source,int index, bool loop)
	{
		source.clip = library[index];
		source.Play();
	}
}
