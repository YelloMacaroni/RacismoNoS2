using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using Image = UnityEngine.UI.Image;

public class Inventory : MonoBehaviourPunCallbacks
{
    [SerializeField] public Transform cam;
    [SerializeField] private bool active = false;
    [SerializeField] public float PlayerActivateDistance;
    public bool[] isFull = new[] { false, false, false };
    public GameObject[] slots = new GameObject[3];
    [SerializeField] public GameObject FlashLight;
    [SerializeField] public GameObject FlashLight2;
    [SerializeField] public Image squareSlot1;
    [SerializeField] public Image squareSlot2;
    [SerializeField] public Image squareSlot3;
    [SerializeField] public Image Slot1;
    [SerializeField] public Image Slot2;
    [SerializeField] public Image Slot3;
    [SerializeField] public Sprite FlashlightSprite;
    [SerializeField] private int SelectedSlot;
    public PhotonView PV;
    public GameObject lampe;
    public AudioSource lampesound;

    

    // Update is called once per frame
    private void Start()
    {
        if(!PhotonNetwork.IsConnected ||photonView.IsMine)
        {SelectedSlot = 1;
        FlashLight.SetActive(false);
        FlashLight2.SetActive(false);
        Slot1.enabled = false;
        Slot2.enabled = false;
        Slot3.enabled = false;
        squareSlot1.enabled = false;
        squareSlot2.enabled = false;
        squareSlot3.enabled = false;
        if (PlayerPrefs.GetString("lampe","False") == "True")
        {
            Slot1.enabled = true;
            Slot1.sprite = FlashlightSprite;
            SelectedSlot = 1;
            squareSlot1.enabled = true;
            slots[0] = FlashLight ;
            isFull[0] = true;
        }
        }
    }

    void Update()
    {
        if(!PhotonNetwork.IsConnected || photonView.IsMine)
        {if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isFull[0])
            {
                SelectedSlot = 1;
                squareSlot1.enabled = true;
                squareSlot2.enabled = false;
                squareSlot3.enabled = false;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isFull[1])
            {
                SelectedSlot = 2;
                squareSlot1.enabled = false;
                squareSlot2.enabled = true;
                squareSlot3.enabled = false;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (isFull[2])
            {
                SelectedSlot = 3;
                squareSlot1.enabled = false;
                squareSlot2.enabled = false;
                squareSlot3.enabled = true;
            }
            
        }
        if (isFull[SelectedSlot-1] && slots[SelectedSlot-1].CompareTag("Flalight") )
        {
            if (PhotonNetwork.IsConnected)
                PV.RPC("RPC_ActiveLamp", RpcTarget.All);
            else
                FlashLight.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                lampesound.Play();
                if (PhotonNetwork.IsConnected)
                    PV.RPC("RPC_OnOffLamp", RpcTarget.All);
                else
                FlashLight.GetComponentInChildren<Light>().enabled =  !FlashLight.GetComponentInChildren<Light>().enabled;
                
            
            }
        }
        else
        {
            if (isFull[SelectedSlot-1])
            {
                if (PhotonNetwork.IsConnected)
                    PV.RPC("RPC_DesactiveLamp", RpcTarget.All);
                else
                    FlashLight.SetActive(false);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.R) && isFull[SelectedSlot - 1])
        {
            slots[SelectedSlot - 1].transform.position = cam.transform.position + cam.transform.forward * 1;
            slots[SelectedSlot - 1].SetActive(true);
            if (slots[SelectedSlot-1].CompareTag("Flalight"))
            {
                if (PhotonNetwork.IsConnected)
                    PV.RPC("RPC_DesactiveLamp", RpcTarget.All);
                else 
                    FlashLight.SetActive(false);
            }
            
            slots[SelectedSlot - 1] = null;
            isFull[SelectedSlot - 1] = false;
            switch (SelectedSlot)
            {
                case 1:
                    Slot1.enabled = false;
                    break;
                case 2:
                    Slot2.enabled = false;
                    break;
                case 3:
                    Slot3.enabled = false;
                    break;
            }
        }

        RaycastHit hit;
        active = Physics.Raycast(cam.position,cam.TransformDirection(Vector3.forward),out hit,PlayerActivateDistance);
        if (active)
        {

            if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("InteractKey","E"))))
            {
                print(hit.transform.tag);
                
                switch (hit.transform.tag)
                {
                    case "Flalight":
                        int i = 0;
                        while (i<3 && isFull[i])
                        {
                            i += 1;
                        }

                        if (i<3)
                        {
                            slots[i] = hit.transform.gameObject ;
                            isFull[i] = true;
                            switch (i)
                            {
                                case 0:
                                    Slot1.enabled = true;
                                    Slot1.sprite = FlashlightSprite;
                                    SelectedSlot = 1;
                                    squareSlot1.enabled = true;
                                    squareSlot2.enabled = false;
                                    squareSlot3.enabled = false;
                                    break;
                                case 1:
                                    Slot2.enabled = true;
                                    Slot2.sprite = FlashlightSprite;
                                    SelectedSlot = 2;
                                    squareSlot1.enabled = false;
                                    squareSlot2.enabled = true;
                                    squareSlot3.enabled = false;
                                    break;
                                case 2:
                                    Slot3.enabled = true;
                                    Slot3.sprite = FlashlightSprite;
                                    SelectedSlot = 3;
                                    squareSlot1.enabled = false;
                                    squareSlot2.enabled = false;
                                    squareSlot3.enabled = true;
                                    break;
                            }
                            slots[i].SetActive(false);
                            PlayerPrefs.SetString("lampe","True");


                        }
                        lampe.SetActive(true);
                        StartCoroutine("Waitforsec");
                        break;
                }
            }
        }
        
        }
    }
    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(2);
        lampe.SetActive(false);   
    }

    [PunRPC]
    void RPC_OnOffLamp(){
        FlashLight.GetComponentInChildren<Light>().enabled =
                    !FlashLight.GetComponentInChildren<Light>().enabled;
    }

    [PunRPC]
    void RPC_ActiveLamp(){
        FlashLight.SetActive(true);
    }
    
    [PunRPC]
    void RPC_DesactiveLamp(){
        FlashLight.SetActive(false);
    }
}
