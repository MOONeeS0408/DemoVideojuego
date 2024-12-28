using UnityEngine;

public class DrawerController : MonoBehaviour
{
    public Animator drawerAnimator; // Referencia al Animator del cajón
    private bool isOpen = false; // Estado del cajón (abierto/cerrado)

    void Update()
    {
        // Abrir el cajón si está cerrado y presionas la tecla O
        if (Input.GetKeyDown(KeyCode.O) && !isOpen)
        {
            OpenDrawer();
        }
        // Cerrar el cajón si está abierto y presionas la tecla C
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            CloseDrawer();
        }
    }

    void OpenDrawer()
    {
        drawerAnimator.SetTrigger("OpenTrigger"); // Activar animación de apertura
        isOpen = true;
    }

    void CloseDrawer()
    {
        drawerAnimator.SetTrigger("CloseTrigger"); // Activar animación de cierre
        isOpen = false;
    }
}
