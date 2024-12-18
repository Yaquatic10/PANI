using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform spawnBulletPoint;
    private Transform playerPosition;
    public float bulletVelocity = 100;
    private IAG iag;

    void Start()
    {
        playerPosition = FindObjectOfType<PlayerMovement>().transform;

        iag = GetComponent<IAG>();

        if (iag == null)
        {
            Debug.LogError("No se encontró el componente IAG en el objeto.");
        }

        Invoke("Shotplayer", 1);
    }

    void Update()
    {
        if (playerPosition != null)
        {
            // Calcula la dirección hacia el jugador en el plano XZ
            Vector3 directionToPlayer = playerPosition.position - spawnBulletPoint.position;
            directionToPlayer.y = 0; // Ignorar la diferencia en altura

            // Ajusta la rotación de spawnBulletPoint para que apunte hacia el jugador en el plano XZ
            spawnBulletPoint.rotation = Quaternion.LookRotation(directionToPlayer);
        }
    }

    void Shotplayer()
    {
        if (playerPosition != null && iag != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition.position);

            // Dispara solo si el enemigo está dentro de la distancia de parada definida en IAG
            if (distanceToPlayer <= iag.stopDistance)
            {
                Vector3 playerDirection = (playerPosition.position - spawnBulletPoint.position).normalized;

                GameObject newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);

                newBullet.GetComponent<Rigidbody>().velocity = playerDirection * bulletVelocity;
            }
        }

        // Invoca el método nuevamente después de 2 segundos
        Invoke("Shotplayer", 2);
    }
}

