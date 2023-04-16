using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu1 : MonoBehaviour
{
    public GameObject settingToActivate;

    public GameObject settingToDesactivate;
    public void Activate(){
        settingToActivate.SetActive(true);
        settingToDesactivate.SetActive(false);
    }

    public void Desactivate(){
        
        settingToActivate.SetActive(false);
        settingToDesactivate.SetActive(true);
    
    }

}
