using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioClip[] musicClip;
    private int musicIndex;

    public AudioMixer audioMixer;  // Referencia al AudioMixer
    private const string sfxVolumeParameter = "SFXVolume"; // El nombre del parámetro en el AudioMixer

    void Start()
    {
        musicIndex = Mathf.Clamp(musicIndex, 0, musicClip.Length - 1);  // Asegura que el índice esté dentro del rango
        if (musicClip.Length > 0)
        {
            musicAudioSource.clip = musicClip[musicIndex];
            musicAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("No music clips assigned!");
        }
    }


    public void SetSFXVolume(float volume)
    {
        // Convierte el valor del slider (0-1) a la escala de volumen (usada por el Audio Mixer)
        float volumeInDb = Mathf.Lerp(-80f, 0f, volume);  // -80 es silencio, 0 es volumen máximo
        audioMixer.SetFloat(sfxVolumeParameter, volumeInDb);
    }

    private void Update()
    {

    }
}