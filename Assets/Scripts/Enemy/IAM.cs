using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAM : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip audioClip; // Clip de audio a reproducir
    public float intervaloRepetición = 5f; // Intervalo de tiempo entre cada reproducción
    private float tiempoUltimaReproducción;

    public float vidaActualE = 1500;
    private float VidaMinima = 0;

    [SerializeField] public float cantidadPuntos;

    private Puntos puntosScript;

    void Start()
    {
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

        // Inicializa el tiempo de la última reproducción
        tiempoUltimaReproducción = Time.time;
    }

    public void DanoM(float putaso)
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

        // Reproducir el audio con un intervalo definido
        if (audioSource != null && audioClip != null && Time.time >= tiempoUltimaReproducción + intervaloRepetición)
        {
            audioSource.PlayOneShot(audioClip);
            tiempoUltimaReproducción = Time.time; // Actualiza el tiempo de la última reproducción
        }
    }
}
