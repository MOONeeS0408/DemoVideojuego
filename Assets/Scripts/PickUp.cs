using UnityEngine;
using TMPro;
public class Pickup : MonoBehaviour
{
    public ItemData itemData; // Información del objeto
    private bool isPlayerNearby = false; 
    public GameObject pickupText;
    [SerializeField] private TMP_Text messageText;
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
            Debug.Log($"{itemData.itemName} añadido al inventario.");
            //Destroy(gameObject); 
            gameObject.SetActive(false);
            messageText.text = $"Se ha añadido \"{itemData.itemName}\" al inventario. Abre el inventario con (I)";
            messageText.gameObject.SetActive(true);
            pickupText.SetActive(false);
            Invoke(nameof(FinishPickUpItem), 5f);
        }
        else
        {
            Debug.LogError("No se encontró el InventoryManager en la escena.");
        }
    }


    private void FinishPickUpItem()
    {

        messageText.text = "";
        messageText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
