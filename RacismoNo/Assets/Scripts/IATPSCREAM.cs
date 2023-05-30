using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;



public class IATPSCREAM : MonoBehaviour
{
    public string sceneName; 
    public AudioSource scream;
    
    public void OnTriggerEnter(Collider other)
    {
        scream.Play();
        StartCoroutine("Waitforsec");
    }
    
    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName); 
    }


   
}
