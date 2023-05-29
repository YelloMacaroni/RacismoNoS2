using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textrdc : MonoBehaviour
{
    public GameObject textecorpmort; 
    public void OnTriggerEnter(Collider other)
    {
        textecorpmort.SetActive(true);
    }

   
    
}
