using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class KeypadCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera; // Cámara FreeLook del jugador
    [SerializeField] private CinemachineVirtualCamera keypadCamera; // Cámara del keypad
    [SerializeField] public GameObject KeypadText; // Referencia al texto de interacción
    [SerializeField] private Transform player; // Jugador
    [SerializeField] private float interactionDistance = 2f; // Distancia para interactuar con el keypad
    [SerializeField] private WallMover wallMover; // Referencia al script WallMover

    private bool isUsingKeypad = false;

    private void Start()
    {
        KeypadText.SetActive(false); // Oculta el texto de interacción al inicio
        
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Muestra el texto si el jugador está dentro de la distancia de interacción
        if (distance <= interactionDistance && !isUsingKeypad)
        {
            KeypadText.SetActive(true); // Muestra el texto de interacción cuando está cerca
           
        }
        else
        {
            KeypadText.SetActive(false); // Oculta el texto si no está cerca
           
        }

        // Permite activar el keypad con la tecla "J" si el jugador está cerca
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.J) && !isUsingKeypad)
        {
            ActivateKeypadCamera();
        }
    }

    public void ActivateKeypadCamera()
    {
        isUsingKeypad = true;
        freeLookCamera.Priority = 0; // Desactiva la cámara FreeLook
        keypadCamera.Priority = 10; // Activa la cámara del keypad
        KeypadText.SetActive(false); // Oculta el texto de interacción durante la interacción
       
    }

    public void DeactivateKeypadCamera()
    {
        isUsingKeypad = false;
        freeLookCamera.Priority = 10; // Reactiva la cámara FreeLook
        keypadCamera.Priority = 0; // Desactiva la cámara del keypad
        KeypadText.SetActive(false); // Oculta el texto de interacción
       
    }

    public void OnAccessGranted()
    {
        if (wallMover != null)
        {
            wallMover.MoveWall(); // Mueve la pared
        }

        DeactivateKeypadCamera(); // Regresa a la cámara principal
    }

    public void OnAccessDenied()
    {
        DeactivateKeypadCamera(); // Devuelve al jugador a la cámara principal si el acceso es denegado
    }
}
