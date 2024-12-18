using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip audioClip; // Clip de audio a reproducir en bucle

    public float vidaActualE = 200;
    private float VidaMinima = 0;

    [SerializeField] public float cantidadPuntos;

    private Puntos puntosScript;

    void Start()
    {
        // Comienza la reproducción del audio en bucle al iniciar
        if (audioSource != null && audioClip != null)
        {
            audioSource.loop = true;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        // Busca el objeto que contiene el script Puntos y asigna la referencia
        GameObject puntosObject = GameObject.FindWithTag("PuntosManager");
        if (puntosObject != null)
        {
            puntosScript = puntosObject.GetComponent<Puntos>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto con el tag 'PuntosManager'.");
        }
    }

    public void Dano(float putaso)
    {
        vidaActualE -= putaso;
        CheckD();
    }

    public void CheckD()
    {
        if (vidaActualE <= VidaMinima)
        {
            if (puntosScript != null)
            {
                puntosScript.SumarPuntos(cantidadPuntos);
            }

            // Detiene la reproducción del audio al desaparecer el enemigo
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        // Si el jugador existe, actualiza el destino del NavMeshAgent
        if (player != null)
        {
            navMeshAgent.destination = player.transform.position;
        }
    }
}
