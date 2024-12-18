using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public TMP_Text timerText;
    [SerializeField, Tooltip("Tiempo en segundos")] private float timerTime;
    [SerializeField] private Canvas canvasToHide; // Referencia al Canvas que se debe ocultar

    private int minutes, seconds, cents;
    private PlayVideoOnTimerEnd playVideoOnTimerEnd;
    private bool isTimerEnded = false;

    private void Start()
    {
        playVideoOnTimerEnd = FindObjectOfType<PlayVideoOnTimerEnd>(); // Buscar la instancia de PlayVideoOnTimerEnd en la escena
    }

    private void Update()
    {
        if (!isTimerEnded)
        {
            timerTime -= Time.deltaTime;

            if (timerTime < 0) timerTime = 0;

            minutes = (int)(timerTime / 60f);
            seconds = (int)(timerTime - minutes * 60);
            cents = (int)((timerTime - (int)timerTime) * 100f);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);

            if (timerTime == 0)
            {
                isTimerEnded = true;
                StopGame(); // Detener el juego
                HideCanvas(); // Esconder el canvas
                if (playVideoOnTimerEnd != null)
                {
                    playVideoOnTimerEnd.PlayVideo(); // Reproducir el video
                }
            }
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0; // Detener la escena
    }

    private void HideCanvas()
    {
        if (canvasToHide != null)
        {
            canvasToHide.enabled = false;
        }
    }
}
