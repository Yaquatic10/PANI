using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool buttonPressed = false; // Indicador de si se ha presionado un botón
    private bool isFirstTimeLoading = true; // Indicador de si es la primera vez que se carga la escena

    private void Start()
    {
        StartCoroutine(WaitForSceneLoad());
    }

    // Método para cargar la escena cuando se presione un botón
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            buttonPressed = true;
        }
    }

    // Método para esperar la carga de la escena y, luego, cargar la siguiente escena
    private IEnumerator WaitForSceneLoad()
    {
        // Esperar la carga de la escena
        while (!SceneManager.GetSceneByName("Objetivo").isLoaded)
        {
            yield return null;
        }

        // Si es la primera vez que se carga la escena, esperar a que se presione un botón antes de cargar la siguiente escena
        if (isFirstTimeLoading)
        {
            while (!buttonPressed)
            {
                yield return null;
            }
        }

        // Cargar la escena "JuegoPAni"
        SceneManager.LoadScene("JuegoPAni");

        // Reanudar la ejecución del juego estableciendo el timeScale en 1
        Time.timeScale = 1f;
    }
}
