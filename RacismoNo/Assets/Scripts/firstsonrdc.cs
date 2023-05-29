using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstsonrdc : MonoBehaviour
{
    public GameObject textecorpmort; 
    public AudioSource crihorreur;
    public Collider dqs;


    public void Allerauniveau()
    {
        crihorreur.Play();
        textecorpmort.SetActive(true);
        dqs.enabled=false;
        StartCoroutine("Waitforsec");
        
       
    }
    public void OnTriggerEnter(Collider other)
    {
        Allerauniveau(); 
    }

    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(3);
        textecorpmort.SetActive(false);
        dqs.enabled=true;
        
    }
    
}
