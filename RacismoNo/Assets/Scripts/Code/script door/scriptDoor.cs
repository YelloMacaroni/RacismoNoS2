using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class scriptDoor : MonoBehaviour
{
    [SerializeField] public Transform cam;
    [SerializeField] public bool keyLab1Owned = false;
    [SerializeField] public bool keyLab2Owned = false;
    [SerializeField] public float PlayerActivateDistance;
    [SerializeField] private bool active = false;
    public GameObject key;
    public GameObject nopdoor;
    public AudioSource keysound;
    public AudioSource door;
 

    
 
    private void Update()
    {
        
        RaycastHit hit;
        
        active = Physics.Raycast(cam.position,cam.TransformDirection(Vector3.forward),out hit,PlayerActivateDistance);
        if (active)
        {
            print(active);
            if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("InteractKey","e"))))
            {
                print(hit.transform.tag); 
                switch (hit.transform.tag)
                {
                    case "toiletteDoor":
                        if (hit.transform.GetComponent<Animator>() != null)
                        {
                            if (hit.transform.GetComponent<Animator>().GetBool("activate"))
                            {
                                hit.transform.GetComponent<Animator>().ResetTrigger(("activate"));
                            }
                            else
                            {
                                hit.transform.GetComponent<Animator>().SetTrigger(("activate"));
                            }
                            
                        }
                        door.Play();
                        break;
                    case "lab 1 door": 
                        if (keyLab1Owned)
                        {
                            if (hit.transform.GetComponent<Animator>() != null)
                            {
                                if (hit.transform.GetComponent<Animator>().GetBool("activate"))
                                {
                                    hit.transform.GetComponent<Animator>().ResetTrigger(("activate"));
                                }
                                else
                                {
                                    hit.transform.GetComponent<Animator>().SetTrigger(("activate"));
                                }
                            
                            }
                            door.Play();
                        }
                        else
                        {
                            nopdoor.SetActive(true);
                            StartCoroutine("Waitforsec");
                            
                        }
                        break;
                    case "lab 2 door":
                        if (keyLab2Owned)
                        {
                            if (hit.transform.GetComponent<Animator>() != null)
                            {
                                if (hit.transform.GetComponent<Animator>().GetBool("activate"))
                                {
                                    hit.transform.GetComponent<Animator>().ResetTrigger(("activate"));
                                }
                                else
                                {
                                    hit.transform.GetComponent<Animator>().SetTrigger(("activate"));
                                }
                                
                            }
                            door.Play();
                        }
                        else
                        {
                            nopdoor.SetActive(true);
                            StartCoroutine("Waitforsec");

                        }
                        break;
                    case "key lab 1":
                        
                        keysound.Play();
                        Destroy(hit.transform.gameObject);
                        keyLab1Owned = true;
                        key.SetActive(true);
                        StartCoroutine("Waitforsec");
                        
                        
                        break;
                    case "key lab 2":
                        
                        keysound.Play();
                        Destroy(hit.transform.gameObject);
                        keyLab2Owned = true;
                        key.SetActive(true);
                        StartCoroutine("Waitforsec");
                        break;
                    default:
                        if (hit.transform.GetComponent<Animator>() != null)
                        {
                            if (hit.transform.GetComponent<Animator>().GetBool("activate"))
                            {
                                hit.transform.GetComponent<Animator>().ResetTrigger(("activate"));
                            }
                            else
                            {
                                hit.transform.GetComponent<Animator>().SetTrigger(("activate"));
                            }
                            
                        }
                        break;
                }
            }
        }
    }
    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(2);
        nopdoor.SetActive(false);
        key.SetActive(false);
    }
   
}

