using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entryspawn : MonoBehaviour
{
    public GameObject textecorpmort; 
    public AudioSource crihorreur;


    public void Allerauniveau()
    {
        crihorreur.Play();
        textecorpmort.SetActive(true);
        StartCoroutine("Waitforsec");
    }
    public void OnTriggerEnter(Collider other)
    {
        Allerauniveau(); 
    }

    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(2);
        textecorpmort.SetActive(false);
        
    }
}
