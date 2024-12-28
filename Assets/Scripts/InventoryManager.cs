using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>(); // Lista de inventario
    public Transform inventoryPanel; 
    public GameObject inventorySlotPrefab; // Prefab de slot del inventario

    public void AddItem(ItemData newItem)
    {
        items.Add(newItem); 
        UpdateInventoryUI(); 
    }

    private void UpdateInventoryUI()
    {

        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

    
        foreach (ItemData item in items)
        {
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);

            //Icono 
            Image icon = newSlot.GetComponentInChildren<Image>();
            if (icon != null)
            {
                icon.sprite = item.itemIcon;
            }

            //Nombre
            TextMeshProUGUI itemName = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            if (itemName != null)
            {
                itemName.text = item.itemName;
                Debug.Log($"Nombre del objeto asignado: {item.itemName}");
            }
            else
            {
                Debug.LogError("No se encontró un componente de texto en el prefab del slot.");
            }

        }
    }

    public void LoadInventory(List<string> itemNames)
    {
        items.Clear();
        foreach (string itemName in itemNames)
        {
            
            ItemData item = FindItemByName(itemName);
            if (item != null)
            {
                items.Add(item);
            }
        }
        UpdateInventoryUI();
    }

    public ItemData FindItemByName(string name)
    {
        
        return Resources.Load<ItemData>($"Items/{name}"); 
    }

}
