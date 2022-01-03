using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;



public class AnimationAndoMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;
    //Variables de control
    [SerializeField] private int life = 3;
    
    //variables para almacenar los valores del player input 
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    int isWalkingHash;
    int isRunningHash;


    bool isMovementPressed;
    bool isRunnPressed;
    [SerializeField] private float rotationPerFrame = 3f;
    [SerializeField] private float runSpeed = 3f;

    //Variables de Gravedad
    [SerializeField] private float gravity = - 9.8f;
    [SerializeField] private float groundedGravity = -05f;

    //Variables para el salto
    bool isJumpPressed = false;
    bool isJumping = false;
    private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight=4f;
    [SerializeField] private float maxJumpTime =0.75f;
    private int isJumpingHash;
    //private int jumpCount = 0;
    private bool isJumpingAnimating = false;

    //INVENTARIO
    private InventoryManager mgInventory;
    private bool haveKey= false;
    private GameManager gameManager;

    //EVENTOS
    public event Action onDeath;

    public static event Action<bool> onKeyNear;
  
        //***********************************************************************
    /* Este diccionario sera utilizado luego para agregar mas animaciones al salto***********************************
     * 
    Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    */ 
    //Variables como isJumpingHash o isRunningHash facilitan el uso de las animaciones ya que reemplaza el string.

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        
        playerInput.CharacterControl.Move.started += onMovementInput;
        playerInput.CharacterControl.Move.canceled += onMovementInput;
        playerInput.CharacterControl.Move.performed += onMovementInput;
        playerInput.CharacterControl.Run.started += onRun;
        playerInput.CharacterControl.Run.canceled += onRun;
        playerInput.CharacterControl.Jump.started += onJump;
        playerInput.CharacterControl.Jump.canceled += onJump;
        SetJumpVariables();
       
    }
    
    void SetJumpVariables()
    {
        float timeToApex = maxJumpTime / 2; //controla el tiempo que se demora en ellegar al punto mas extremo.
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
    void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash , true);
            isJumpingAnimating = true;
            isJumping = true;
            currentMovement.y = initialJumpVelocity * 0.75f;
            currentRunMovement.y = initialJumpVelocity*0.75f;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;

        }
    }
    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log(isJumpPressed);
    }
    void onRun(InputAction.CallbackContext context)
    {
        isRunnPressed = context.ReadValueAsButton();
    }
    //////////////////////////START_START_START_START_/////////////////////////////////////
    ///
    private void Start()
    {
        mgInventory = GetComponent<InventoryManager>();
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleAnimation();
        HandleRotation();
        

        if (isRunnPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        
      
        HandleGravity();
        HandleJump();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseItem();
        }
      

    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runSpeed;
        currentRunMovement.z = currentMovementInput.y * runSpeed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void HandleAnimation()
    {
        //Tomar parametros de el animador.
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
        //corre si movimiento y correr son verdaderos y si no esta corriendo actualemente
        if ((isMovementPressed && isRunnPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        //Termina de correr si movimiento o correr son falsos y esta corriendo actualmente
        else if ((!isMovementPressed || !isRunnPressed)&& isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }
    void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if (characterController.isGrounded)
        {
            if (isJumpingAnimating) 
            {
                animator.SetBool(isJumpingHash, false);
                isJumpingAnimating = false;
            }

            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousVelocity = currentMovement.y;
            float newVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextVelocity = Mathf.Max((previousVelocity + newVelocity) * .5f, -20.0f);
            currentMovement.y = nextVelocity;
            currentRunMovement.y = nextVelocity;
        }
        else
        {
            float previousVelocity = currentMovement.y;
            float newVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextVelocity = (previousVelocity + newVelocity) * 0.5f;
            currentMovement.y = nextVelocity;
            currentRunMovement.y = nextVelocity;
        }
    }
    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;


        if (isMovementPressed)
        {
            //crea una rotacion basada en la direccion actual que el jugador esta presionando
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationPerFrame * Time.deltaTime);
        }

    }
    private void OnEnable()
    {
        playerInput.CharacterControl.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterControl.Disable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            life = life -1;
            


            if (life ==0)
            {
                onDeath();
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("VOID"))
        {
            Destroy(gameObject);
        }


        if (other.gameObject.CompareTag("KEY"))
        {
            GameObject Key = other.gameObject;
            Key.SetActive(false);
            mgInventory.AddInventoryOne(Key);
            haveKey = true;
            onKeyNear?.Invoke(false);
        }
        if (other.gameObject.CompareTag("HEART"))
        {
            GameObject heart = other.gameObject;
            heart.SetActive(false);
            gameManager.AddLife();
           
        }
        if (other.gameObject.CompareTag("COIN"))
        {
            GameObject coin = other.gameObject;
            coin.SetActive(false);
            gameManager.AddScore();
            
            
        }
        if (other.gameObject.CompareTag("DOR") && haveKey == true)
        {
            GameObject door = other.gameObject;
            door.SetActive(false);
        }
       
        
    }
    private void UseItem()
    {
        GameObject key = mgInventory.GetInventoryOne();
        key.SetActive(true);
        key.transform.position = transform.position + new Vector3(0F, 1.2F, 0F);
            
    }
    

}
