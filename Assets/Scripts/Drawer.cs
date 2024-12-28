using UnityEngine;

public class Drawer : MonoBehaviour
{
    public string requiredItemID; // ID del objeto necesario para abrir el cajón
    public Animator drawerAnimator; // Referencia al Animator del cajón
    private bool isPlayerNearby = false; // Para saber si el jugador está cerca
    private bool isOpen = false; // Estado del cajón (abierto/cerrado)

    void Update()
    {
        // Si el jugador está cerca y presiona la tecla O y no está abierto
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.O) && !isOpen)
        {
            TryOpenDrawer();
        }
        // Si el cajón está abierto y presionas la tecla C, ciérralo
        else if (isPlayerNearby && Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            TryCloseDrawer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void TryOpenDrawer()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager != null)
        {
            // Verifica si el inventario contiene el objeto necesario
            bool hasRequiredItem = inventoryManager.items.Exists(item => item.itemID == requiredItemID);
            Debug.Log($"¿Tiene el objeto necesario? {hasRequiredItem}");

            if (hasRequiredItem)
            {
                // Activa la animación de apertura y cambia el estado del cajón a abierto
                drawerAnimator.SetTrigger("OpenTrigger");
                isOpen = true; // El cajón está abierto
                Debug.Log("Cajón abierto.");
            }
            else
            {
                Debug.Log("No tienes el objeto necesario para abrir este cajón.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el InventoryManager en la escena.");
        }
    }


    void TryCloseDrawer()
    {
        // Activa la animación de cierre y cambia el estado del cajón a cerrado
        drawerAnimator.SetTrigger("CloseTrigger");
        isOpen = false; // El cajón está cerrado
        Debug.Log("Cajón cerrado.");
    }
}
