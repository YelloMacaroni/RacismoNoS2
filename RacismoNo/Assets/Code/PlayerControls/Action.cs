using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    
    CharacterController characterController;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 15f;
    public float crouchingSpeed = 3.75f;

    float gravity = 20f;
    Vector3 moveDirection;
    private bool isRunning = false;
    public bool isCrouching=false;
    public bool isWalking=true;

    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();  
        
    }
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        
        if (Input.GetKey(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKey(KeyCode.LeftControl))
            isCrouching=true;
        else
            {
                isCrouching=false;
                isRunning=false;
            }
        
        if (isRunning)
        {
            speedX  *= runningSpeed;
            speedZ  *= runningSpeed;
        }
        else if(isCrouching)
        {
            speedX *= crouchingSpeed;
            speedZ *= crouchingSpeed;
        }
        else 
        {
            speedX *= walkingSpeed;
            speedZ *= walkingSpeed;
        }
        moveDirection = forward * speedZ + right * speedX;
        characterController.Move(moveDirection * Time.deltaTime);
    }
    public void shift()
    {

    }
    public void ctrl()
    {

    }
    public void esc()
    {

    }
    public void e()
    {

    }
    public void f()
    {

    }
    public void click()
    {

    }

}
