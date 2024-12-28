using UnityEngine;
using TMPro;  // Si usas TextMeshPro

public class ResolutionNavigator : MonoBehaviour
{
    // Referencia al componente Text para mostrar la resolución
    public TextMeshProUGUI resolutionText;  // Cambia a Text si no usas TMP

    private int currentResolutionIndex;

    private Resolution[] customResolutions = new Resolution[]
{
    //new Resolution { width = 3840, height = 2160 }, // 4K UHD
    //new Resolution { width = 2560, height = 1440 }, // QHD
    
    new Resolution { width = 800, height = 600 },
    new Resolution { width = 1024, height = 768 }, // 4:3
    new Resolution { width = 1280, height = 720 }, // HD
    new Resolution { width = 1280, height = 800 }, // 16:10
    new Resolution { width = 1366, height = 768 },
    new Resolution { width = 1600, height = 900 },
    new Resolution { width = 1920, height = 1080 }, // FHD

    
};

    void Start()
    {
        // Establecer la resolución por defecto al iniciar
        Screen.SetResolution(1920, 1080, true);  // 1920x1080 en pantalla completa
        Debug.Log("Initial resolution set to 1920x1080 in fullscreen.");

        // Inicializar la resolución actual en el índice correspondiente
        currentResolutionIndex = GetCurrentResolutionIndex();

        // Actualizar el texto con la resolución actual al iniciar
        UpdateResolutionText();
    }

    public void NextResolution()
    {
        // Incrementar el índice y validar que no exceda el límite
        currentResolutionIndex = Mathf.Clamp(currentResolutionIndex + 1, 0, customResolutions.Length - 1);
        ApplyResolution();
    }

    public void PreviousResolution()
    {
        // Decrementar el índice y validar que no sea menor a 0
        currentResolutionIndex = Mathf.Clamp(currentResolutionIndex - 1, 0, customResolutions.Length - 1);
        ApplyResolution();
    }

    private void ApplyResolution()
    {
        // Aplicar la resolución actual
        Resolution res = customResolutions[currentResolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        Debug.Log($"Resolution changed to: {res.width}x{res.height}");

        // Actualizar el texto con la nueva resolución
        UpdateResolutionText();
    }

    private int GetCurrentResolutionIndex()
    {
        // Buscar la resolución actual en la lista de resoluciones
        Resolution current = Screen.currentResolution;
        for (int i = 0; i < customResolutions.Length; i++)
        {
            if (customResolutions[i].width == current.width && customResolutions[i].height == current.height)
            {
                return i;
            }
        }
        return 0; // Por defecto, retornar el índice 0 si no se encuentra
    }

    private void UpdateResolutionText()
    {
        // Actualizar el texto en la UI con la resolución actual
        Resolution res = customResolutions[currentResolutionIndex];
        resolutionText.text = $"{res.width} x {res.height}";  // Actualiza el texto con el formato deseado
    }
}
