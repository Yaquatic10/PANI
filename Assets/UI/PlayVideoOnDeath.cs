using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnDeath : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referencia al VideoPlayer
    public Canvas canvasToShow; // Referencia al Canvas que se debe mostrar después del video
    public Canvas canvasToHide; // Referencia al Canvas que se debe ocultar

    private bool isVideoPlayed = false;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Suscribir al evento
        }

        // Ocultar el canvasToShow al inicio
        if (canvasToShow != null)
        {
            canvasToShow.enabled = false;
        }
    }

    public void PlayVideo()
    {
        if (!isVideoPlayed && videoPlayer != null)
        {
            isVideoPlayed = true;

            // Ocultar el canvasToHide
            HideCanvas();

            videoPlayer.Play();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        videoPlayer.Pause(); // Pausar el video al final
        EnableCursor(); // Reactivar el cursor
        ShowCanvas(); // Mostrar el canvasToShow al finalizar el video
    }

    private void ShowCanvas()
    {
        if (canvasToShow != null)
        {
            // Verificar si el video se ha reproducido completamente
            if (videoPlayer.isPlaying)
            {
                // Si el video todavía se está reproduciendo, espera un poco antes de mostrar el canvasToShow
                Invoke(nameof(ShowCanvas), 0.5f); // Intenta mostrar el canvasToShow nuevamente después de un breve retardo
                return;
            }

            // Si el video ha terminado de reproducirse completamente, muestra el canvasToShow
            canvasToShow.enabled = true;
        }
    }

    // Método para ocultar el canvasToHide (por si acaso)
    private void HideCanvas()
    {
        if (canvasToHide != null)
        {
            canvasToHide.enabled = false;
        }
    }

    // Método para reactivar el cursor
    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
