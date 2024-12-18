using System.Collections;
using UnityEngine;

public class AudioFadeOutManager : MonoBehaviour
{
    public float fadeOutDuration = 1f; // Duración del fade out en segundos

    // Método público para iniciar el fade out del audio
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutAllAudioSources());
    }

    // Corrutina para realizar el fade out de todos los AudioSource en la escena
    IEnumerator FadeOutAllAudioSources()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        float[] startVolumes = new float[audioSources.Length];

        // Guardar los volúmenes iniciales
        for (int i = 0; i < audioSources.Length; i++)
        {
            startVolumes[i] = audioSources[i].volume;
        }

        float currentTime = 0f;

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = currentTime / fadeOutDuration;

            // Reducir el volumen de cada AudioSource
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].volume = Mathf.Lerp(startVolumes[i], 0f, alpha);
            }

            yield return null;
        }

        // Detener todos los AudioSource
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Stop();
            audioSources[i].volume = startVolumes[i];
        }
    }
}
