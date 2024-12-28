using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class KeypadCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera; 
    [SerializeField] private CinemachineVirtualCamera keypadCamera; 
    [SerializeField] public GameObject KeypadText; 
    [SerializeField] private Transform player; 
    [SerializeField] private float interactionDistance = 2f; 
    [SerializeField] private WallMover wallMover;

    private bool isUsingKeypad = false;

    private void Start()
    {
        KeypadText.SetActive(false); 
        
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
     
        if (distance <= interactionDistance && !isUsingKeypad)
        {
            KeypadText.SetActive(true); 
           
        }
        else
        {
            KeypadText.SetActive(false); 
           
        }

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.J) && !isUsingKeypad)
        {
            ActivateKeypadCamera();
        }
    }

    public void ActivateKeypadCamera()
    {
        isUsingKeypad = true;
        freeLookCamera.Priority = 0; 
        keypadCamera.Priority = 10; // Activa la cámara del keypad
        KeypadText.SetActive(false); 
       
    }

    public void DeactivateKeypadCamera()
    {
        isUsingKeypad = false;
        freeLookCamera.Priority = 10; // Reactiva la cámara FreeLook
        keypadCamera.Priority = 0; 
        KeypadText.SetActive(false);
       
    }

    public void OnAccessGranted()
    {
        if (wallMover != null)
        {
            wallMover.MoveWall(); 
        }

        DeactivateKeypadCamera(); // Regresa a la cámara principal
    }

    public void OnAccessDenied()
    {
        DeactivateKeypadCamera(); // Devuelve al jugador a la cámara principal si el acceso es denegado
    }
}
