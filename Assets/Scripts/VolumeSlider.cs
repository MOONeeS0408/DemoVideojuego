using NavKeypad;
using UnityEngine;
using UnityEngine.UI;  // Asegúrate de importar este espacio de nombres

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;  // Slider para controlar el volumen
    public Keypad keypad;       // Referencia al script Keypad

    void Start()
    {
        // Establecer el valor inicial del slider (puedes usar un valor predeterminado)
        volumeSlider.value = 0.5f;  // Por ejemplo, el volumen a la mitad

        // Conectar el slider al método SetSFXVolume en Keypad
        volumeSlider.onValueChanged.AddListener(keypad.SetSFXVolume);
    }
}
