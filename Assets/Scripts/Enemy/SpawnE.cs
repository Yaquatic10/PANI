using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnE : MonoBehaviour
{
    private float rangoGeneracion = 33f;
    public List<EnemigoProbabilidad> enemigos; // Lista de objetos de enemigos con sus probabilidades de aparecer

    private void Start()
    {
        StartCoroutine(GenerarEnemigos());
    }

    private IEnumerator GenerarEnemigos()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.1f, 1f)); // Espera un tiempo aleatorio antes de generar el siguiente enemigo

            float probabilidadTotal = 0f;
            foreach (EnemigoProbabilidad enemigoProbabilidad in enemigos)
            {
                probabilidadTotal += enemigoProbabilidad.probabilidadAparecer;
            }

            float probabilidadAleatoria = Random.Range(0f, probabilidadTotal);
            float probabilidadAcumulada = 0f;
            foreach (EnemigoProbabilidad enemigoProbabilidad in enemigos)
            {
                probabilidadAcumulada += enemigoProbabilidad.probabilidadAparecer;
                if (probabilidadAleatoria <= probabilidadAcumulada)
                {
                    GameObject enemigoSeleccionado = enemigoProbabilidad.enemigo;

                    float posXGeneracion = Random.Range(-rangoGeneracion, rangoGeneracion);
                    float posYGeneracion = Random.Range(-rangoGeneracion, rangoGeneracion);

                    Vector3 posAleatoria = new Vector3(posXGeneracion, .5f, posYGeneracion);
                    Instantiate(enemigoSeleccionado, posAleatoria, enemigoSeleccionado.transform.rotation);

                    break;
                }
            }
        }
    }
}

[System.Serializable]
public class EnemigoProbabilidad
{
    public GameObject enemigo;
    public float probabilidadAparecer;
}