using UnityEngine;
using TMPro;  

public class ResolutionNavigator : MonoBehaviour
{
   
    public TextMeshProUGUI resolutionText;  

    private int currentResolutionIndex;

    private Resolution[] customResolutions = new Resolution[]
{
    //new Resolution { width = 3840, height = 2160 }, 
    //new Resolution { width = 2560, height = 1440 }, 
    
    new Resolution { width = 800, height = 600 },
    new Resolution { width = 1024, height = 768 }, 
    new Resolution { width = 1280, height = 720 }, 
    new Resolution { width = 1280, height = 800 }, 
    new Resolution { width = 1366, height = 768 },
    new Resolution { width = 1600, height = 900 },
    new Resolution { width = 1920, height = 1080 }, 

    
};

    void Start()
    {
        // Establecer la resolución por defecto al iniciar
        Screen.SetResolution(1920, 1080, true);  // 1920x1080 en pantalla completa
        Debug.Log("Initial resolution set to 1920x1080 in fullscreen.");
        currentResolutionIndex = GetCurrentResolutionIndex();
        UpdateResolutionText();
    }

    public void NextResolution()
    {
        currentResolutionIndex = Mathf.Clamp(currentResolutionIndex + 1, 0, customResolutions.Length - 1);
        ApplyResolution();
    }

    public void PreviousResolution()
    {
        currentResolutionIndex = Mathf.Clamp(currentResolutionIndex - 1, 0, customResolutions.Length - 1);
        ApplyResolution();
    }

    private void ApplyResolution()
    {
        Resolution res = customResolutions[currentResolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        Debug.Log($"Resolution changed to: {res.width}x{res.height}");
        UpdateResolutionText();
    }

    private int GetCurrentResolutionIndex()
    {
        Resolution current = Screen.currentResolution;
        for (int i = 0; i < customResolutions.Length; i++)
        {
            if (customResolutions[i].width == current.width && customResolutions[i].height == current.height)
            {
                return i;
            }
        }
        return 0; 
    }

    private void UpdateResolutionText()
    {
 
        Resolution res = customResolutions[currentResolutionIndex];
        resolutionText.text = $"{res.width} x {res.height}"; 
    }
}
