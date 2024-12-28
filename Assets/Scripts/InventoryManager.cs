using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Necesario si usas TextMeshPro
public class InventoryManager : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>(); // Lista de objetos en el inventario
    public Transform inventoryPanel; // Aquí asignarás el objeto vacío que tiene el GridLayoutGroup
    public GameObject inventorySlotPrefab; // Prefab para cada slot en el inventario

    public void AddItem(ItemData newItem)
    {
        items.Add(newItem); // Añadir el objeto a la lista
        UpdateInventoryUI(); // Actualizar la interfaz gráfica
    }

    private void UpdateInventoryUI()
    {
        // Limpia el inventario anterior
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Crea nuevos slots para cada objeto en la lista
        foreach (ItemData item in items)
        {
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);

            // Asigna el ícono del objeto
            Image icon = newSlot.GetComponentInChildren<Image>();
            if (icon != null)
            {
                icon.sprite = item.itemIcon;
            }

            // Asigna el nombre del objeto
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
            // Busca el objeto por su nombre/ID (deberías tener un sistema para registrar todos los items disponibles)
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
        // Este método debe buscar entre los objetos disponibles del juego
        return Resources.Load<ItemData>($"Items/{name}"); // Ejemplo: Cargar desde una carpeta de Resources
    }

}
