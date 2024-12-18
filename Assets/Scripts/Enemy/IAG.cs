using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAG : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip audioClip; // Clip de audio a reproducir
    public float intervaloRepetici�n = 5f; // Intervalo de tiempo entre cada reproducci�n
    private float tiempoUltimaReproducci�n;

    public float vidaActualE = 500;
    private float VidaMinima = 0;
    public float stopDistance = 10f; // Distancia a la que el enemigo se detiene del jugador
    [SerializeField] public float cantidadPuntos;

    private Puntos puntosScript;
    private GameObject player;

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
            Debug.LogError("No se encontr� el objeto con el tag 'PuntosManager'.");
        }
        player = GameObject.FindWithTag("Player");

        // Inicializa el tiempo de la �ltima reproducci�n
        tiempoUltimaReproducci�n = Time.time;
    }

    public void DanoG(float putaso)
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

            // Detiene la reproducci�n del audio al desaparecer el enemigo
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Si la distancia al jugador es mayor que la distancia de parada, mueve el agente
            if (distanceToPlayer > stopDistance)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.destination = player.transform.position;
            }
            else
            {
                // Det�n el agente cuando est� dentro de la distancia de parada
                navMeshAgent.isStopped = true;
            }
        }

        // Reproducir el audio con un intervalo definido
        if (audioSource != null && audioClip != null && Time.time >= tiempoUltimaReproducci�n + intervaloRepetici�n)
        {
            audioSource.PlayOneShot(audioClip);
            tiempoUltimaReproducci�n = Time.time; // Actualiza el tiempo de la �ltima reproducci�n
        }
    }
}
