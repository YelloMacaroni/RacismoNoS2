using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mdp : MonoBehaviour
{
    [SerializeField] public Text Ans;
    private string Answer = "42";
    public GameObject keyboard;
   

    public AudioSource True;
    public AudioSource wrong;
    

     
    public void Number(int number)
    {
        Ans.text +=number.ToString();
    }
    
    public void Execute()
    {
        if(Ans.text==Answer)
        {
            Ans.text="Correct";
            True.Play();
            StartCoroutine("Waitforsec");
            Cursor.visible=false;
            Cursor.lockState = CursorLockMode.Locked;
            
            
        }
        else
        {
            Ans.text="";
            wrong.Play();
        }
    }
    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(1);
        keyboard.SetActive(false);
    }
}
