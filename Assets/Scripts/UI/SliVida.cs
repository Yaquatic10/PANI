using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliVida : MonoBehaviour
{
    public Slider vida;
    public float vidaMaxima = 100;
    public float vidaMinima = 0;
    private float vidaActual;
    public float regenracionEnTiempo = .9f;
    public float regenraciom = 3;

    private float tope = 100;
    private PlayVideoOnDeath playVideoOnDeath;
    public AudioSource audioSource; // AudioSource para la música
    public AudioClip[] hitSounds; // Lista de clips de audio

    public List<GameObject> objectsToDestroy; // Lista de objetos para destruir asignados en el inspector

    public GameObject lanzallamas; // Objeto que se hará invisible al final del juego

    private void Start()
    {
        vidaActual = vidaMaxima;
        vida.maxValue = vidaMaxima;
        vida.value = vidaMaxima;

        playVideoOnDeath = FindObjectOfType<PlayVideoOnDeath>(); // Buscar la instancia de PlayVideoOnDeath en la escena
    }

    public void PerdidaV(float dano1)
    {
        vidaMaxima -= dano1;
        StartCoroutine(regenCorutine());
        vida.value = vidaMaxima;

        // Reproducir un sonido al azar de la lista
        PlayRandomHitSound();
    }

    void Update()
    {
        vida.value = vidaMaxima;
        CheckVi();
    }

    public IEnumerator regenCorutine()
    {
        yield return new WaitForSeconds(2);

        while (vidaMaxima < tope)
        {
            vidaMaxima += regenraciom;
            vida.value = vidaMaxima;

            yield return new WaitForSeconds(regenracionEnTiempo);
        }
    }

    public void CheckVi()
    {
        if (vidaMaxima <= 0)
        {
            if (playVideoOnDeath != null)
            {
                playVideoOnDeath.PlayVideo(); // Llamar al método PlayVideo del script PlayVideoOnDeath
            }

            MuteAudio(); // Mutea el audio inmediatamente
            DestroyAssignedObjects(); // Destruye los objetos asignados

            StartCoroutine(EndGame());
        }
    }

    void MuteAudio()
    {
        if (audioSource != null)
        {
            audioSource.mute = true; // Mutea el audio
        }
    }

    void DestroyAssignedObjects()
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                if (Application.isEditor)
                {
                    Debug.Log("Destruyendo objeto en el Editor: " + obj.name);
                    DestroyImmediate(obj, true);
                }
                else
                {
                    Debug.Log("Destruyendo objeto en el Juego: " + obj.name);
                    Destroy(obj);
                }
            }
            else
            {
                Debug.LogWarning("Un objeto en la lista de objetos a destruir es nulo.");
            }
        }
    }

    IEnumerator EndGame()
    {
        // Hacer invisible el lanzallamas
        if (lanzallamas != null)
        {
            lanzallamas.SetActive(false);
        }

        // Esperar un momento antes de detener la escena
        yield return new WaitForSeconds(2f);

        // Detener la escena (puedes pausar el juego aquí si lo prefieres)
        Time.timeScale = 0f;
    }

    // Método para reproducir un sonido al azar de la lista
    void PlayRandomHitSound()
    {
        if (hitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            audioSource.PlayOneShot(hitSounds[randomIndex]);
        }
    }
}
