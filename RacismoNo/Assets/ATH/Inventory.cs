using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Inventory : MonoBehaviour
{
    [SerializeField] public Transform cam;
    [SerializeField] private bool active = false;
    [SerializeField] public float PlayerActivateDistance;
    public bool[] isFull = new[] { false, false, false };
    public GameObject[] slots = new GameObject[3];
    
    [SerializeField] public Image squareSlot1;
    [SerializeField] public Image squareSlot2;
    [SerializeField] public Image squareSlot3;
    [SerializeField] public Image Slot1;
    [SerializeField] public Image Slot2;
    [SerializeField] public Image Slot3;
    [SerializeField] public Sprite Flashlight;
    [SerializeField] private int SelectedSlot;
    // Update is called once per frame
    private void Start()
    {
        SelectedSlot = 1;
        Slot1.enabled = false;
        Slot2.enabled = false;
        Slot3.enabled = false;
        squareSlot1.enabled = false;
        squareSlot2.enabled = false;
        squareSlot3.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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

        if (Input.GetKeyDown(KeyCode.R) && isFull[SelectedSlot - 1])
        {
            slots[SelectedSlot - 1].transform.position = cam.transform.position + cam.transform.forward * 1;
            slots[SelectedSlot - 1].SetActive(true);
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

            if (Input.GetKeyDown(KeyCode.E))
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
                                    Slot1.sprite = Flashlight;
                                    SelectedSlot = 1;
                                    squareSlot1.enabled = true;
                                    squareSlot2.enabled = false;
                                    squareSlot3.enabled = false;
                                    break;
                                case 1:
                                    Slot2.enabled = true;
                                    Slot2.sprite = Flashlight;
                                    SelectedSlot = 2;
                                    squareSlot1.enabled = false;
                                    squareSlot2.enabled = true;
                                    squareSlot3.enabled = false;
                                    break;
                                case 2:
                                    Slot3.enabled = true;
                                    Slot3.sprite = Flashlight;
                                    SelectedSlot = 3;
                                    squareSlot1.enabled = false;
                                    squareSlot2.enabled = false;
                                    squareSlot3.enabled = true;
                                    break;
                            }
                            hit.transform.gameObject.SetActive(false);
                            
                        }
                        break;
                }
            }
        }
        
        
    }
    
    
}
