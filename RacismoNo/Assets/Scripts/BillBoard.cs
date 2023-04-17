using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class BillBoard : MonoBehaviourPunCallbacks
{
	Camera cam;
	public GameObject pseudo;

	void Start(){
		if (photonView.IsMine)
			pseudo.SetActive(false);
	}
	void Update()
	{
		if(cam == null)
			cam = FindObjectOfType<Camera>();

		if(cam == null)
			return;

		transform.LookAt(cam.transform);
		transform.Rotate(Vector3.up * 180);
	}
}