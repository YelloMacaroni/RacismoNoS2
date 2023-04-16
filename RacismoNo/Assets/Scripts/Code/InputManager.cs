using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour {


	Event keyEvent;
	public TMP_Text buttonTextCrouch;
	public GameObject buttonTextCrouch2;
	public GameObject CrouchWaiting;
    public  TMP_Text buttonTextSprint;
	public GameObject  buttonTextSprint2;
	public GameObject SprintWaiting;
    public TMP_Text buttonTextInteract;
	public GameObject buttonTextInteract2;
	public GameObject InteractWaiting;
	KeyCode newKey;

	bool waitingForKey;

	

	void Start ()
	{
		buttonTextCrouch2.SetActive(true);
		CrouchWaiting.SetActive(false);
		buttonTextSprint2.SetActive(true);
		SprintWaiting.SetActive(false);
		buttonTextInteract2.SetActive(true);
		InteractWaiting.SetActive(false);
		waitingForKey = false;
		buttonTextSprint.text = PlayerPrefs.GetString("SprintKey","left shift");
		buttonTextCrouch.text = PlayerPrefs.GetString("CrouchKey","left ctrl");
		buttonTextInteract.text = PlayerPrefs.GetString("InteractKey","e");
		
	}


	void OnGUI()
	{

		keyEvent = Event.current;
		if(keyEvent.isKey && waitingForKey)
		{
			newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
			waitingForKey = false;
		}
		
	}

	/*Buttons cannot call on Coroutines via OnClick().
	 * Instead, we have it call StartAssignment, which will
	 * call a coroutine in this script instead, only if we
	 * are not already waiting for a key to be pressed.
	 */
	public void StartAssignment(string keyName)
	{
		if(!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}

	//Assigns buttonText to the text component of
	//the button that was pressed

	//Used for controlling the flow of our below Coroutine
	IEnumerator WaitForKey()
	{
		while(!keyEvent.isKey)
			yield return null;
	}

	/*AssignKey takes a keyName as a parameter. The
	 * keyName is checked in a switch statement. Each
	 * case assigns the command that keyName represents
	 * to the new key that the user presses, which is grabbed
	 * in the OnGUI() function, above.
	 */
	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;


		yield return WaitForKey();


		switch(keyName)
		{
		case "Sprint":
			buttonTextSprint.text = newKey.ToString(); //set button text to new key
			PlayerPrefs.SetString("SprintKey", newKey.ToString()); //save new key to playerprefs
			buttonTextSprint2.SetActive(true);
			SprintWaiting.SetActive(false);
			break;
		case "Crouch":
			buttonTextCrouch.text = newKey.ToString(); //set button text to new key
			PlayerPrefs.SetString("CrouchKey", newKey.ToString()); //save new key to playerprefs
			buttonTextCrouch2.SetActive(true);
			CrouchWaiting.SetActive(false);
			break;
		case "Interact":
			buttonTextInteract.text = newKey.ToString(); //set button text to new key
			PlayerPrefs.SetString("InteractKey", newKey.ToString()); //save new key to playerprefs
			buttonTextInteract2.SetActive(true);
			InteractWaiting.SetActive(false);
			break;
		}

		yield return null;
	}

	public void Desactivate(string keyName)
	{

		
		switch(keyName)
			{
			case "Sprint":
				buttonTextSprint2.SetActive(false);
				SprintWaiting.SetActive(true);
				break;
			case "Crouch":
				buttonTextCrouch2.SetActive(false);
				CrouchWaiting.SetActive(true);
				break;
			case "Interact":
				buttonTextInteract2.SetActive(false);
				InteractWaiting.SetActive(true);
				break;
			}
	}

	
}