using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Necesario para usar la barra de progreso
using static UnityEngine.Rendering.DebugUI;

namespace NavKeypad
{
    public class Keypad : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;
        [Header("Combination Code (9 Numbers Max)")]
        [SerializeField] private int keypadCombo = 12345;

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted";
        [SerializeField] private string accessDeniedText = "Denied";

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f;
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;
        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f); //orangy
        [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f); //red
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f); //greenish
        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;
        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;

        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer audioMixer; // Asigna tu AudioMixer aquí
        [SerializeField] private string sfxVolumeParameter = "SFX"; // Nombre del parámetro en el mixer para el volumen
        public AudioManager audioManager;  // Referencia al AudioManager

        // Agregar una referencia a la barra de progreso
        [Header("Progress Bar")]
        [SerializeField] private Image progressBar; // La barra de progreso UI (Image con un fill amount)

        private string currentInput;
        private bool displayingResult = false;
        private bool accessWasGranted = false;
        private float progress = 1f; // Representa el progreso de la barra (1 = 100%, 0 = 0%)

        [SerializeField] private TMP_Text percentageText; // Referencia al texto del porcentaje
        private float progressBarValue = 1f; // Empieza llena (1 = 100%)
        [SerializeField] private GameObject gameOverPanel; // Panel de Game Over

        private void Awake()
        {
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
        }

        // Método para controlar el volumen de SFX
        public void SetSFXVolume(float volume)
        {
            if (audioManager != null)
            {
                audioManager.SetSFXVolume(volume);  // Llama al método en AudioManager
            }
        }

        //Gets value from pressedbutton
        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
            if (displayingResult || accessWasGranted) return;
            switch (input)
            {
                case "enter":
                    CheckCombo();
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 9) // 9 max passcode size 
                    {
                        return;
                    }
                    currentInput += input;
                    keypadDisplayText.text = currentInput;
                    break;
            }
        }

        public void CheckCombo()
        {
            if (int.TryParse(currentInput, out var currentKombo))
            {
                bool granted = currentKombo == keypadCombo;
                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted));
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input for some reason..");
            }
        }

        //mainly for animations 
        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted) AccessGranted();
            else AccessDenied();

            yield return new WaitForSeconds(displayResultTime);
            displayingResult = false;
            if (granted) yield break;
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
        }

        public UIManager uiManager;  // Referencia al UIManager

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            onAccessDenied?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource.PlayOneShot(accessDeniedSfx);

            // Disminuye el valor de la barra de progreso
            progressBarValue -= 0.5f; // 10% de disminución
            progressBarValue = Mathf.Clamp(progressBarValue, 0f, 1f); // Asegúrate de que el valor esté entre 0 y 1

            UpdateProgressBar(progressBarValue); // Actualiza la barra y el porcentaje

            // Si llega al 0%, muestra el panel de Game Over
            if (progressBarValue <= 0f)
            {
                uiManager.ShowGameOverPanel();  // Muestra el panel de Game Over
            }
        }

        private void ShowGameOver()
        {
            // Activa el panel de Game Over
            gameOverPanel.SetActive(true);

            // Detener cualquier audio o animación si es necesario
            audioSource.Stop(); // Detener el sonido actual, por ejemplo
        }


        private void UpdateProgressBar(float value)
        {
            // Actualiza el valor de la barra de progreso
            progressBar.fillAmount = value;

            // Calcula el porcentaje
            int percentage = Mathf.RoundToInt(value * 100);

            // Actualiza el texto para mostrar el porcentaje
            percentageText.text = $"{percentage}%";
        }


        private void ClearInput()
        {
            currentInput = "";
            keypadDisplayText.text = currentInput;
        }

        private void AccessGranted()
        {
            accessWasGranted = true;
            keypadDisplayText.text = accessGrantedText;
            onAccessGranted?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
            audioSource.PlayOneShot(accessGrantedSfx);
        }
    }
}
