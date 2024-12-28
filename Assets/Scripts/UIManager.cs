using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Camera mainCamera;               // Cámara principal
    public Camera notebookCamera;           // Cámara para ver el modelo 3D
    public GameObject notebookPanel;        // Referencia al UI de la libreta de notas
    public GameObject inventoryPanel;       // Referencia al UI del inventario
    public GameObject hudPanel;             // Referencia al HUD Panel principal
    public GameObject pausePanel;           // Referencia al panel de pausa
    public GameObject settingsPanel;
    public GameObject gameOverPanel;

    private bool isNotebookActive = false;  // Estado de la libreta
    private bool isInventoryActive = false; // Estado del inventario


    private void Start()
    {
        Play();
    }

    void Update()
    {
        // Detectar teclas para abrir la libreta o el inventario
        if (Input.GetKeyDown(KeyCode.N)) // Presiona N para abrir la libreta
        {
            if (isNotebookActive)
            {
                DeactiveView();
                isNotebookActive = false;
            }
            else
            {
                ActivateNotebookView();
                isNotebookActive = true;
                isInventoryActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) // Presiona I para abrir el inventario
        {
            if (isInventoryActive)
            {
                DeactiveView();
                isInventoryActive = false;
            }
            else
            {
                ActivateInventoryView();
                isInventoryActive = true;
                isNotebookActive = false;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                ShowHUD();
            }
            else
            {
                Pause();
            }
        }
    }

    public void CleanPanel()
    {
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
        notebookPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void CleanCamera()
    {
        mainCamera.gameObject.SetActive(false);
        notebookCamera.gameObject.SetActive(false);
    }

    public void ShowHUD()
    {
        CleanPanel();
        hudPanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        CleanPanel();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Play()
    {
        CleanCamera();
        CleanPanel();
        mainCamera.gameObject.SetActive(true);
        hudPanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void ActivateNotebookView()
    {
        CleanPanel();
        CleanCamera();
        notebookCamera.gameObject.SetActive(true);
        notebookPanel.SetActive(true);
    }

    public void ActivateInventoryView()
    {
        CleanPanel();
        CleanCamera();
        notebookCamera.gameObject.SetActive(true);
        inventoryPanel.SetActive(true);
    }

    public void DeactiveView()
    {
        CleanPanel();
        CleanCamera();
        mainCamera.gameObject.SetActive(true);
        hudPanel.SetActive(true);
    }

    public void ReturnToPausePanel()
    {
        CleanPanel();
        pausePanel.SetActive(true);         
        Time.timeScale = 0;                 
    }
    public void ShowSettingsPanel()
    {
        CleanPanel();
        settingsPanel.SetActive(true);   
    }
    public void ShowGameOverPanel()
    {
        CleanPanel();
        gameOverPanel.SetActive(true);  
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("MainMenu"); 
    }


    private float SFXVolume;
    private float musicVolume;
    public Slider SFXSlider;
    public Slider MusicSlider;
    public AudioMixer AudioMixer;

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        AudioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        AudioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
