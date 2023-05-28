using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;



public class scriptDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] public Transform cam;
    [SerializeField] public bool keyLab1Owned = false;
    [SerializeField] public bool keyLab2Owned = false;
    [SerializeField] public bool keyelevatormoinsun = false;
    [SerializeField] public float PlayerActivateDistance;
    [SerializeField] private bool active = false;
    [SerializeField] public bool canacessrdc=false;
    public GameObject key;
    public GameObject nopdoor;
    public GameObject card;
    public GameObject elevator; 
    public AudioSource keysound;
    public AudioSource door;
    public AudioSource cardsound;
    public AudioSource elevatorsound;
    public TMP_Text PrincipalQuest;
    public TMP_Text SecondaryQuest;
    bool quest1 = false;
    bool quest2 = false;    
    public string sceneName;
    public PhotonView PV; 

    public void Start()
    {
        if (!PhotonNetwork.IsConnected || photonView.IsMine)
        
        {if ((SceneManager.GetActiveScene()).name == "Floor -1")
            PrincipalQuest.text = "Leave the basement";
        if ((SceneManager.GetActiveScene()).name == "Spawn1")
            PrincipalQuest.text = "Enter the building";}
    }
 
    private void Update()
    {
        if (!PhotonNetwork.IsConnected || photonView.IsMine)
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
                        if (!PhotonNetwork.IsConnected)
                        {if (hit.transform.GetComponent<Animator>() != null)
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
                        door.Play();}
                        else{
                            GameObject tempo = hit.transform.gameObject;
                            PV.RPC("RPC_OnOffDoor", RpcTarget.All,tempo.GetComponent<PhotonView>().ViewID);
                        }
                        break;
                    case "lab 1 door": 
                        if (keyLab1Owned)
                        {
                            if (!PhotonNetwork.IsConnected)
                            {if (hit.transform.GetComponent<Animator>() != null)
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
                            door.Play();}
                            else{
                             GameObject tempo = hit.transform.gameObject;
                            PV.RPC("RPC_OnOffDoor", RpcTarget.All,tempo.GetComponent<PhotonView>().ViewID);
                        }
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
                            if (!PhotonNetwork.IsConnected)
                            {if (hit.transform.GetComponent<Animator>() != null)
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
                            door.Play();}
                            else{
                            GameObject tempo = hit.transform.gameObject;
                            PV.RPC("RPC_OnOffDoor", RpcTarget.All,tempo.GetComponent<PhotonView>().ViewID);
                        }
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
                    case "key elevator -1":
                        cardsound.Play();
                        Destroy(hit.transform.gameObject);
                        keyelevatormoinsun = true;
                        card.SetActive(true);
                        StartCoroutine("Waitforsec");
                        break;
                    case "elevator -1": 
                        if (keyelevatormoinsun)
                        {
                            elevatorsound.Play();     
                            StartCoroutine("Waitforsec");
                            if (!PhotonNetwork.IsConnected)
                                SceneManager.LoadScene(sceneName);
                            else
                                photonView.RPC("RPC_Teleportation", RpcTarget.MasterClient,sceneName);        
                        }
                        else
                        {
                            SecondaryQuest.text = "Find the card";
                            elevator.SetActive(true);
                            StartCoroutine("Waitforsec");
                        }
                        quest1 = true;
                       
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
        }}
    }
    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(2);
        nopdoor.SetActive(false);
        key.SetActive(false);
        card.SetActive(false);
        elevator.SetActive(false);
    }

    [PunRPC]
    void RPC_OnOffDoor(int id){
        GameObject hit = PhotonNetwork.GetPhotonView(id).gameObject;
        if (hit.GetComponent<Animator>() != null)
                        {
                            if (hit.GetComponent<Animator>().GetBool("activate"))
                            {
                                hit.GetComponent<Animator>().ResetTrigger(("activate"));
                            }
                            else
                            {
                                hit.GetComponent<Animator>().SetTrigger(("activate"));
                            }
                            
                        }
                        door.Play();
    }


    [PunRPC]
    void RPC_Teleportation(string Scene){
            PhotonNetwork.LoadLevel(Scene);    
        }
   
}

