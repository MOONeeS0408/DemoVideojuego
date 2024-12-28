using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController controller;
    private Animator animator; // Referencia al Animator
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 5f; // Velocidad de rotación para suavizado
    [SerializeField] private float smoothingFactor = 5f; // Factor de suavización de velocidad
    private Transform cameraTransform;

    private float currentSpeed = 0f; // Velocidad actual del jugador

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();

    
    }

    void Update()
    {
        // Verifica si el jugador está en el suelo
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Obtén el movimiento del jugador desde el InputManager
        Vector2 movementInput = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        // Ajusta el movimiento a la orientación de la cámara
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f; // Ignora la inclinación vertical
        cameraRight.y = 0f;

        Vector3 adjustedMove = (cameraForward * move.z + cameraRight * move.x).normalized;

        // Suavizar la dirección de movimiento
        if (adjustedMove != Vector3.zero)
        {
            Vector3 smoothedDirection = Vector3.Slerp(transform.forward, adjustedMove, Time.deltaTime * rotationSpeed);
            gameObject.transform.forward = smoothedDirection;
        }

        // Suavizar la velocidad del jugador
        float targetSpeed = adjustedMove.magnitude * playerSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothingFactor);

        // Movimiento del jugador
        controller.Move(adjustedMove * Time.deltaTime * currentSpeed);

        // Actualiza la animación según la velocidad
        animator.SetFloat("Speed", currentSpeed);

        // Manejo de la gravedad
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
        
    

}
