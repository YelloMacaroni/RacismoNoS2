using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

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
    public TMP_Text PrincipalQuest;
    public TMP_Text SecondaryQuest;
    bool quest1 = false;
    bool quest2 = false;
    

    public void Start()
    {
        if ((SceneManager.GetActiveScene()).name == "Floor -1")
            PrincipalQuest.text = "Leave the basement";
        if ((SceneManager.GetActiveScene()).name == "Spawn1")
            PrincipalQuest.text = "Enter the building";
    }
 
    private void Update()
    {
        RaycastHit hit;
        
        active = Physics.Raycast(cam.position,cam.TransformDirection(Vector3.forward),out hit,PlayerActivateDistance);
        if (active)
        {
            if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("InteractKey","E"))))
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
                            if (quest2)
                                SecondaryQuest.text = "Find the right keys";
                            else
                            {
                                SecondaryQuest.text = "Find the right key";
                            }
                            quest1 = true;
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
                            if (quest1)
                                SecondaryQuest.text = "Find the right keys";
                            else
                            {
                                SecondaryQuest.text = "Find the right key";
                            }
                            quest2 = true;
                            nopdoor.SetActive(true);
                            StartCoroutine("Waitforsec");

                        }
                        break;
                    case "key lab 1":
                        if (quest1)
                            {if (quest2)
                                {SecondaryQuest.text = "Find the other key";
                                quest1 = false;}
                            else
                            {
                                SecondaryQuest.text = "";
                                quest1 = false;
                            }}
                        keysound.Play();
                        Destroy(hit.transform.gameObject);
                        keyLab1Owned = true;
                        key.SetActive(true);
                        StartCoroutine("Waitforsec");
                        
                        
                        break;
                    case "key lab 2":
                        if (quest2)
                            {if (quest1)
                                {SecondaryQuest.text = "Find the other key";
                                quest2 = false;}
                            else
                            {
                                SecondaryQuest.text = "";
                                quest2 = false;
                            }}
                        keysound.Play();
                        Destroy(hit.transform.gameObject);
                        keyLab2Owned = true;
                        key.SetActive(true);
                        StartCoroutine("Waitforsec");
                        break;
                    default:
                        if (!(hit.transform.tag == "Flalight"))
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

