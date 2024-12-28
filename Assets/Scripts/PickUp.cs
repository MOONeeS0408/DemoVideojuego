using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ItemData itemData; // Información del objeto
    private bool isPlayerNearby = false; 
    public GameObject pickupText; 

    void Update()
    {
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
            pickupText.SetActive(true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            pickupText.SetActive(false);
        }
    }


    void CollectItem()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>(); // Encuentra el InventoryManager
        if (inventoryManager != null)
        {
            inventoryManager.AddItem(itemData); // Añade el objeto al inventario
            Debug.Log($"{itemData.itemName} recogido y añadido al inventario.");
            Destroy(gameObject); 
            pickupText.SetActive(false); 
        }
        else
        {
            Debug.LogError("No se encontró el InventoryManager en la escena.");
        }
    }

}
