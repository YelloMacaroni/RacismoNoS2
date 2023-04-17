using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
//BONJOUR 
public class MultiMove : MonoBehaviourPunCallbacks
{
    Animator animator;
    public bool CanMove { get; private set;}=true;  
    private bool isSprinting => canSprint && Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("SprintKey","LeftShift")));
    private bool shouldCrouch => canCrouch && Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("CrouchKey","LeftControl"))) && !duringCrouchAnimation && characterController.isGrounded;
    
    
    [Header("Functional Options")]
    [SerializeField]private bool canSprint=true;
    [SerializeField]private bool canCrouch=true;
    [SerializeField]private bool canUseHeadBob=true;
    [SerializeField]private bool useFootsteps =true;



    [Header("Animations")]
    [SerializeField]int isWalkingHash;
    [SerializeField]int isWalkingHashBack;
    [SerializeField]int isWalkingHashRight;
    [SerializeField]int isWalkingHashLeft;
    
    [SerializeField]int isRunningHash;
    [SerializeField]int isRunningHashBack;
    [SerializeField]int isRunningHashRight;
    [SerializeField]int isRunningHashLeft;
    
    [SerializeField]int isCrouchingHash;
    [SerializeField]int isCrouchingHashFront;
    [SerializeField]int isCrouchingHashBack;
    [SerializeField]int isCrouchingHashRight;
    [SerializeField]int isCrouchingHashLeft;

    [Header("Movement Parameters")]
    [SerializeField]private float walkSpeed = 3.0f;
    [SerializeField]private float sprintSpeed = 6.0f;
    [SerializeField]private float crouchSpeed = 1.5f;
    [SerializeField]private float gravity = 30.0f;
    
    [Header("Look Parameters")]
    [SerializeField,Range(1,10)]private float lookSpeedX=2.0f;
    [SerializeField,Range(1,10)]private float lookSpeedY=2.0f;
    [SerializeField,Range(1,180)]private float upperLookLimit=80.0f;
    [SerializeField,Range(1,180)]private float lowerLookLimit=80.0f;

    [Header("Crouch Parameters")]
    [SerializeField]private float crouchHeight=0.5f;
    [SerializeField]private float StandingHeight=2f;
    [SerializeField]private float timetoCrouch=0.25f;
    [SerializeField]private Vector3 crouchingCenter= new Vector3(0,0.5f,0);
    [SerializeField]private Vector3 standingCenter= new Vector3(0,0,0);

    [Header("Headbob Parameters")]
    [SerializeField]private float walkBobSpeed = 14f;
    [SerializeField]private float walkBobAmount = 0.05f;
    [SerializeField]private float sprintBobSpeed = 18f;
    [SerializeField]private float sprintBobAmount = 0.11f;
    [SerializeField]private float crouchBobSpeed = 8f;
    [SerializeField]private float crouchBobAmount = 0.025f;


    [Header("Footsteps Parameters")]
    [SerializeField]private float baseStepSpeed= 0.5f;
    [SerializeField]private float crouchStepMultipler = 1.5f;
    [SerializeField]private float sprintStepMultipler = 0.6f;
    [SerializeField]private AudioSource footstepAudioSource=default;
    [SerializeField]private AudioClip [] woodClips= default;
    [SerializeField]private AudioClip [] grassClips= default;
    [SerializeField]private AudioClip [] metalClips= default;
    private float footstepTimer=0;
    private float GetCurrentOffSet => isCrouching ? baseStepSpeed * crouchStepMultipler : isSprinting? baseStepSpeed * sprintStepMultipler:baseStepSpeed;
    private float defaultYPos=0;
    private float timer;
    
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject playerCameraToDestroy;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX=0;

    private void Start()
    {
        animator=GetComponent<Animator>();
        isWalkingHash=Animator.StringToHash("canWalk");
        isWalkingHashBack=Animator.StringToHash("canWalkback");
        isWalkingHashRight=Animator.StringToHash("canWalkRight");
        isWalkingHashLeft=Animator.StringToHash("canWalkLeft");
        
        isRunningHash=Animator.StringToHash("canRun");
        isRunningHashBack=Animator.StringToHash("canRunBack");
        isRunningHashRight=Animator.StringToHash("canRunRight");
        isRunningHashLeft=Animator.StringToHash("canRunLeft");

        isCrouchingHash=Animator.StringToHash("Crouch");
        isCrouchingHashFront=Animator.StringToHash("canCrouch");
        isCrouchingHashRight=Animator.StringToHash("canCrouchRight");
        isCrouchingHashBack=Animator.StringToHash("canCrouchBack");
        isCrouchingHashLeft=Animator.StringToHash("canCrouchLeft");
        
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!photonView.IsMine)
        {
            Destroy(playerCameraToDestroy);
        }
    }


    void Update()
    {
        if (photonView.IsMine)
        {
            if (Cursor.visible == false)
            {
                if (CanMove)
                {
                    HandleMovementInput();
                    HandleAnimations();
                    HandleMouseLook();
                    if (canCrouch)
                        HandleCrouch();
                    if (canUseHeadBob)
                        HandleHeadbob();
                    if (useFootsteps)
                        Handle_Footsteps();
                    ApplyFinalMovements();
                } 
            }
        }
    }
    
    private void HandleMovementInput()
    {
        currentInput=new Vector2((isCrouching ? crouchSpeed:isSprinting ? sprintSpeed  :walkSpeed)*Input.GetAxis("Vertical"),( isCrouching ? crouchSpeed:isSprinting ? sprintSpeed  : walkSpeed)*Input.GetAxis("Horizontal"));
        float moveDirectionY= moveDirection.y;
        moveDirection=(transform.TransformDirection(Vector3.forward)*currentInput.x)+(transform.TransformDirection(Vector3.right)*currentInput.y);
        moveDirection.y=moveDirectionY;
     
    }

    private void HandleAnimations()
    {
        bool isWalking=animator.GetBool(isWalkingHash);
        bool isWalkingBack=animator.GetBool(isWalkingHashBack);
        bool isWalkingRight=animator.GetBool(isWalkingHashRight);
        bool isWalkingLeft=animator.GetBool(isWalkingHashLeft);
        
        bool isRunning=animator.GetBool(isRunningHash);
        bool isRunningBack=animator.GetBool(isRunningHashBack);
        bool isRunningRight=animator.GetBool(isRunningHashRight);
        bool isRunningLeft=animator.GetBool(isRunningHashLeft);
        
        bool isCrouch=animator.GetBool(isCrouchingHash);
        bool isCrouchFront=animator.GetBool(isCrouchingHashFront);
        bool isCrouchBack=animator.GetBool(isCrouchingHashBack);
        bool isCrouchRight=animator.GetBool(isCrouchingHashRight);
        bool isCrouchLeft=animator.GetBool(isCrouchingHashLeft);

        bool forwardPressed=(Input.GetKey("w"));
        bool backwardPressed=(Input.GetKey("s"));
        bool rightPressed=(Input.GetKey("d"));
        bool leftPressed=(Input.GetKey("a"));
        bool runPressed=Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("SprintKey","LeftShift")));
       
        
        
        if (!isWalking && forwardPressed)
            animator.SetBool("canWalk",true);
        if (isWalking && !forwardPressed)
            animator.SetBool("canWalk",false);
        
        if (!isWalkingBack && backwardPressed)
            animator.SetBool("canWalkback",true);
        if (isWalkingBack && !backwardPressed)
            animator.SetBool("canWalkback",false);

        if (!isWalkingRight && rightPressed)
            animator.SetBool("canWalkRight",true);
        if (isWalkingRight && !rightPressed)
            animator.SetBool("canWalkRight",false);

        if (!isWalkingLeft && leftPressed)
            animator.SetBool("canWalkLeft",true);
        if (isWalkingLeft && !leftPressed)
            animator.SetBool("canWalkLeft",false);
        
        if (!isRunning && forwardPressed && runPressed)
            animator.SetBool("canRun",true);
        if (isRunning && (!forwardPressed || !runPressed))
            animator.SetBool("canRun",false);
        
        if (!isRunningBack && backwardPressed && runPressed)
            animator.SetBool("canRunBack",true);
        if (isRunningBack && (!backwardPressed || !runPressed))
            animator.SetBool("canRunBack",false);

        if (!isRunningRight && rightPressed && runPressed)
            animator.SetBool("canRunRight",true);
        if (isRunningRight && (!rightPressed || !runPressed))
            animator.SetBool("canRunRight",false);

        if (!isRunningLeft && leftPressed && runPressed)
            animator.SetBool("canRunLeft",true);
        if (isRunningLeft && (!leftPressed || !runPressed))
            animator.SetBool("canRunLeft",false);
        
        
        
        if (!isCrouch && isCrouching)
            animator.SetBool("Crouch",true);
        if (isCrouch && !isCrouching)
            animator.SetBool("Crouch",false);

        if (!isCrouchFront && isCrouching && forwardPressed)
            animator.SetBool("canCrouch",true);
        if (isCrouchFront &&  (!isCrouching|| !forwardPressed))
            animator.SetBool("canCrouch",false);

        if (!isCrouchRight && isCrouching && rightPressed)
            animator.SetBool("canCrouchRight",true);
        if (isCrouchRight &&  (!isCrouching || !rightPressed))
            animator.SetBool("canCrouchRight",false);

        if (!isCrouchBack && isCrouching && backwardPressed)
            animator.SetBool("canCrouchBack",true);
        if (isCrouchBack &&  (!isCrouching || !backwardPressed))
            animator.SetBool("canCrouchBack",false);

        if (!isCrouchLeft && isCrouching && leftPressed)
            animator.SetBool("canCrouchLeft",true);
        if (isCrouchLeft &&  (!isCrouching || !leftPressed))
            animator.SetBool("canCrouchLeft",false);
        
        
    }
    
    private void HandleMouseLook()
    {
        rotationX-=Input.GetAxis("Mouse Y")*lookSpeedY;
        rotationX=Mathf.Clamp(rotationX,-upperLookLimit,lowerLookLimit);
        playerCamera.transform.localRotation=Quaternion.Euler(rotationX,0,0);
        transform.rotation*=Quaternion.Euler(0,Input.GetAxis("Mouse X")*lookSpeedX,0);
    }
    
    private void HandleCrouch()
    {
        if(shouldCrouch)
            StartCoroutine(CrouchStand());
    }
    
    public Slider slider;

    public void SettingsSentitivity()
    {
        lookSpeedX = slider.value;
        lookSpeedY = slider.value;
    }
    private IEnumerator CrouchStand()
    {
        if(isCrouching && Physics.Raycast(playerCamera.transform.position,Vector3.up,1f))
            yield break;
        
        duringCrouchAnimation=true;
        
        float timeElapsed=0;
        float targetHeight= isCrouching ? StandingHeight : crouchHeight;
        float currentHeight= characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;
        
        while(timeElapsed<timetoCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight,targetHeight,timeElapsed/timetoCrouch);
            characterController.center= Vector3.Lerp(currentCenter,targetCenter,timeElapsed/timetoCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        characterController.height=targetHeight;
        characterController.center=targetCenter;
        if(!Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("CrouchKey","left ctrl"))))
            isCrouching = !isCrouching;
        duringCrouchAnimation = false;
    }
    private void HandleHeadbob()
    {
        if (!characterController.isGrounded)return;

        if(Mathf.Abs(moveDirection.x)>0.1f || Mathf.Abs(moveDirection.z)>0.1f)
        {
            timer+=Time.deltaTime*(isCrouching ? crouchBobSpeed:isSprinting?sprintBobSpeed:walkBobSpeed);
            playerCamera.transform.localPosition=new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos+Mathf.Sin(timer)*(isCrouching ? crouchBobAmount : isSprinting? sprintBobAmount:walkBobAmount),
                playerCamera.transform.localPosition.z);
        }
    }
    private void Handle_Footsteps()
    {
        if(!characterController.isGrounded) return;
        if(currentInput==Vector2.zero)return;
        footstepTimer-=Time.deltaTime;
        if(footstepTimer<=0)
            {
                footstepAudioSource.pitch = Random.Range(0.9f, 1.1f);
                if(Physics.Raycast(characterController.transform.position,Vector3.down,out RaycastHit hit,3))
                {
                    switch (hit.collider.tag)
                    {
                        case "Footsteps/WOOD":
                            footstepAudioSource.PlayOneShot(woodClips[Random.Range(0,woodClips.Length-1)]);
                            break;
                        case "Footsteps/METAL":
                            footstepAudioSource.PlayOneShot(metalClips[Random.Range(0,metalClips.Length-1)]);
                            break;
                        case "Footsteps/GRASS":
                            footstepAudioSource.PlayOneShot(grassClips[Random.Range(0,grassClips.Length-1)]); 
                            break;
                        default:
                            footstepAudioSource.PlayOneShot(metalClips[Random.Range(0,metalClips.Length-1)]);
                            break;
                        
                    }
                }
                footstepTimer=GetCurrentOffSet;
            }
    }
    private void ApplyFinalMovements()
    {
        if(!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection*Time.deltaTime);
    }
    
    
    
    

}

