using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ItemData itemData; // Información del objeto
    private bool isPlayerNearby = false; // Verifica si el jugador está cerca
    public GameObject pickupText; // Referencia al texto de interacción

    void Update()
    {
        // Si el jugador está cerca y presiona la tecla E, recoge el objeto
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            pickupText.SetActive(true); // Muestra el texto
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            pickupText.SetActive(false); // Oculta el texto
        }
    }


    void CollectItem()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>(); // Encuentra el InventoryManager
        if (inventoryManager != null)
        {
            inventoryManager.AddItem(itemData); // Añade el objeto al inventario
            Debug.Log($"{itemData.itemName} recogido y añadido al inventario.");
            Destroy(gameObject); // Elimina el objeto del mundo++++++++++++++++++++++++++++++
            pickupText.SetActive(false); // Oculta el texto
        }
        else
        {
            Debug.LogError("No se encontró el InventoryManager en la escena.");
        }
    }

}
