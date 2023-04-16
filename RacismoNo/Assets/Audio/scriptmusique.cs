using UnityEngine;
using System.Collections;

public class scriptmusique : MonoBehaviour {
	
	public bool keepPlaying = false;
	public static scriptmusique NSPlay;
	
	void Awake()
	{
		if(NSPlay == null)
		{
			NSPlay = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			var audioSource = NSPlay.GetComponent<AudioSource>();
			if(keepPlaying)
			{
				if(!audioSource.isPlaying)
				{
					audioSource.Play();
				}
			}
			else
			{
				if(audioSource.isPlaying)
				{
					audioSource.Stop();
				}
			}
			Destroy(gameObject);
		}
	}
}