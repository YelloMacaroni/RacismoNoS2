using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitia : MonoBehaviour
{
    public GameObject iaPrefab;
    public GameObject cube;
    void Start()
    {
        StartCoroutine(ApparitionIA(3f));
    }

    IEnumerator ApparitionIA(float delai)
    {
        yield return new WaitForSeconds(delai);
        Instantiate(iaPrefab, cube.transform.position, cube.transform.rotation);
    }
}
