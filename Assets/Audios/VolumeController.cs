using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public List<AudioSource> audioSources = new List<AudioSource>(); // Lista de AudioSources a controlar
    [Range(0f, 1f)]
    public float volumeIncrement = 0.1f; // Incremento de volumen al presionar el botón

    public void IncreaseVolume()
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume = Mathf.Clamp01(source.volume + volumeIncrement); // Incrementar el volumen dentro del rango [0, 1]
        }
    }
}
