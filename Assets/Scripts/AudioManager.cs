using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;
	public AudioClip bounceAudioClip;
	private AudioSource audioSource;
	// Use this for initialization
	void Awake ()
	{
		instance = this;
		audioSource = Camera.main.GetComponent<AudioSource>();
	}

	public void playBallBounceSound()
	{
		audioSource.PlayOneShot(bounceAudioClip);
	}
}
