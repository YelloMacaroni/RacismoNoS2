<<<<<<< Updated upstream
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
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptmusique : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource source;
    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source= GetComponent<AudioSource>();
    }
    void start()
    {
        source.clip=sound;
    }
    void Update()
    {
        if(Input.GetKeyDown("space"))
            dd();
    }
    private void dd()
    {
        if(!source.isPlaying)
            source.Play();
        else 
            source.Pause();
    }
}
>>>>>>> Stashed changes
