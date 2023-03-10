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
