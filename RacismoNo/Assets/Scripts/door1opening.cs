using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;


public class door1opening : MonoBehaviourPunCallbacks
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
                    if (!PhotonNetwork.IsConnected)
                        
                        {
                            if (transform.GetComponent<Animator>().GetBool("activate"))
                                    transform.GetComponent<Animator>().ResetTrigger(("activate"));
                            else
                                transform.GetComponent<Animator>().SetTrigger(("activate"));
                        }
                      
                        
                        else
                        {
                            GameObject tempo = transform.gameObject;
                        }
                }
              
    
}
