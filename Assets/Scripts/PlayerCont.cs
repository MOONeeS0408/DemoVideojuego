using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController controller;
    private Animator animator; 
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float smoothingFactor = 5f; 
    private Transform cameraTransform;

    private float currentSpeed = 0f; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();

    
    }

    void Update()
    {
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f; 
        cameraRight.y = 0f;

        Vector3 adjustedMove = (cameraForward * move.z + cameraRight * move.x).normalized;

       
        if (adjustedMove != Vector3.zero)
        {
            Vector3 smoothedDirection = Vector3.Slerp(transform.forward, adjustedMove, Time.deltaTime * rotationSpeed);
            gameObject.transform.forward = smoothedDirection;
        }

        float targetSpeed = adjustedMove.magnitude * playerSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothingFactor);

        controller.Move(adjustedMove * Time.deltaTime * currentSpeed);

        animator.SetFloat("Speed", currentSpeed);
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
        
    

}
