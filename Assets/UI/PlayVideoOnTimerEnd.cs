using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideoOnTimerEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referencia al VideoPlayer
    public Canvas canvasToHide; // Referencia al Canvas que se debe ocultar
    public string sceneToLoadAfterVideo; // Nombre de la escena a cargar después del video
    private bool isVideoPlayed = false;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Suscribir al evento
        }
    }

    public void PlayVideo()
    {
        if (!isVideoPlayed && videoPlayer != null)
        {
            isVideoPlayed = true;
            HideCanvas();
            videoPlayer.Play();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        EnableCursor(); // Habilitar el cursor
        RestartGame(); // Reiniciar el juego cargando la escena
    }

    private void HideCanvas()
    {
        if (canvasToHide != null)
        {
            canvasToHide.enabled = false;
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1; // Asegurarse de que el tiempo esté normalizado
        SceneManager.LoadScene(sceneToLoadAfterVideo); // Cargar la escena designada
    }

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
