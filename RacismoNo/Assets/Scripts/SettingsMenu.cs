using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer mainMixer ;
    public void SetVolume(float Volume){
        mainMixer.SetFloat("Volume",Volume + 20);
    }
    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    public void SetAmbiant(bool isFullWhite){
        if(isFullWhite){
            RenderSettings.ambientLight = Color.white;
        }
        else{
            RenderSettings.ambientLight = new Color(0.022f,0.000f,0.047f,1.000f);
        }
    }
}
