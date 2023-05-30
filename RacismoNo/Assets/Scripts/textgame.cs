using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textgame : MonoBehaviour
{

    public GameObject start;
   
    
    // Start is called before the first frame update
    void Start()
    {
        start.SetActive(true);
        StartCoroutine("Waitforsec");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(2);
        start.SetActive(false);
        
    }
}
