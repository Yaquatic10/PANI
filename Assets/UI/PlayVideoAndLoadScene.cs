using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideoAndLoadScene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referencia al VideoPlayer
    public string sceneToLoad; // Nombre de la escena a cargar después del video

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Suscribir al evento
        }
    }

    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            SkipVideo();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadScene();
    }

    void SkipVideo()
    {
        videoPlayer.Stop();
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad); // Cargar la escena
    }
}
