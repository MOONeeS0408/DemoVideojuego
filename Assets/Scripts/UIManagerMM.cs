using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerMM : MonoBehaviour
{
    public GameObject mainMenuPanel;      // Panel principal del menú
    public GameObject settingsPanel;     // Panel de ajustes del menú

    void Start()
    {
        ShowMainMenuPanel();
    }

    public void CleanPanel()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowMainMenuPanel()
    {
        CleanPanel();
        mainMenuPanel.SetActive(true);    // Activa el panel del menú principal
    }

    public void ShowSettingsPanel()
    {
        CleanPanel();
        settingsPanel.SetActive(true);   // Activa el panel de configuraciones
    }

    public void Salir()
    {
        Application.Quit();
    }




    private float SFXVolume;
    private float musicVolumne;
    public Slider SFXSlider;
    public Slider MusicSlider;
    public AudioMixer AudioMixer;
    public void SetMusicVolume(float volume)
    {
        musicVolumne = volume;
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
