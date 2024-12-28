using UnityEngine;
using System.Collections.Generic;

public class BookManager : MonoBehaviour
{
    public static BookManager Instance; 

    [SerializeField] private Renderer leftPageRenderer;  //  página izquierda
    [SerializeField] private Renderer rightPageRenderer; // página derecha
    [SerializeField] private Material defaultMaterial;   // Material por defecto para la página

    [SerializeField] private List<Material> pageMaterials = new List<Material>();  // Lista de materiales para las páginas
    public int currentPageIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Método para agregar un material a la lista
    public void AddPageMaterial(Material material)
    {
        pageMaterials.Add(material);
        UpdatePageMaterials(); 
    }

    // Método para actualizar las texturas de las páginas con los materiales de la lista
    public void UpdatePageMaterials()
    {
        // Si el índice actual es válido
        if (currentPageIndex < pageMaterials.Count)
        {
            // Si el índice es par (0, 2, 4...), asigna la página izquierda
            if (currentPageIndex % 2 == 0)
            {
                leftPageRenderer.material = pageMaterials[currentPageIndex];
            }
            else
            {
                leftPageRenderer.material = defaultMaterial;  // Si no tiene material, usa el por defecto
            }

            // Si el índice es impar (1, 3, 5...), asigna la página derecha
            if ((currentPageIndex + 1) < pageMaterials.Count && (currentPageIndex + 1) % 2 != 0)
            {
                rightPageRenderer.material = pageMaterials[currentPageIndex + 1];
            }
            else
            {
                rightPageRenderer.material = defaultMaterial;  // Si no hay material, asigna el por defecto
            }
        }
    }

    // Método para pasar a la siguiente página (derecha)
    public void NextPage()
    {
        if (currentPageIndex < pageMaterials.Count - 1)
        {
            currentPageIndex++;
            UpdatePageMaterials(); 
        }
    }

    // Método para retroceder a la página anterior (izquierda)
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePageMaterials(); 
        }
    }

    public int GetCurrentPage()
    {
        return currentPageIndex;
    }

    public void SetCurrentPage(int pageIndex)
    {
        currentPageIndex = pageIndex;
        UpdatePageMaterials();
    }

}

