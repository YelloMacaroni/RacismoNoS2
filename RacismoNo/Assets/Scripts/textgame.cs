using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textgame : MonoBehaviour
{

    public GameObject start;
    public GameObject dsq;
    
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
        yield return new WaitForSeconds(5);
        start.SetActive(false);
        yield return new WaitForSeconds(2);
        dsq.SetActive(true);
        yield return new WaitForSeconds(4);
        dsq.SetActive(false);
    }
}
