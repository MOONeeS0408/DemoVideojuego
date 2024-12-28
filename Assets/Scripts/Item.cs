using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName; // Nombre del objeto
    public Sprite itemIcon; // Imagen que aparecerá en el inventario
    public string itemID; // ID único del objeto
}

