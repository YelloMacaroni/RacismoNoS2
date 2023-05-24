using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class invisiblebody : MonoBehaviourPunCallbacks
{
    Renderer ren;
    // Start is called before the first frame update
    void Start()
    {
        ren=GetComponent<Renderer>();
        if (!PhotonNetwork.IsConnected)
            ren.enabled=false;
        else if (photonView.IsMine)
            ren.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
