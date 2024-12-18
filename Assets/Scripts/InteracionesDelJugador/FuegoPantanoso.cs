using System.Collections;
using UnityEngine;

public class FuegoPantanoso : MonoBehaviour
{
    public Transform PuntoDeDisparo;
    public GameObject FuegoPrefab;
    public float VelocidadBala = 1500f;
    public float CadenciaDeDisparo = 0.03f;
    public float TiempoMaximoPresionado = 10f;
    public float cantidadCalorDisparo = 1f;
    public bool disparando;
    public bool armaBloqueada = false;
    public float tiempoPresionado = 0f;
    private HotBar heatBar;
    private AudioSource audioSource;
    public float fadeOutDuration = 1f; // Duración del fade out en segundos

    void Start()
    {
        heatBar = FindObjectOfType<HotBar>();
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource
        audioSource.loop = true; // Hacer que el audio se reproduzca en loop
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !armaBloqueada)
        {
            disparando = true;
            tiempoPresionado = 0f; // Reiniciar el tiempo presionado al iniciar un disparo      
            StartCoroutine(DisparoContinuo());
            if (!audioSource.isPlaying)
            {
                audioSource.volume = 1f; // Asegurar que el volumen esté al máximo
                audioSource.Play(); // Reproducir el sonido en loop al iniciar el disparo
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            disparando = false;
            heatBar.SetAmountToUpdate(0);
            if (audioSource.isPlaying)
            {
                StartCoroutine(FadeOut(audioSource, fadeOutDuration)); // Iniciar el fade out
            }
        }

        if (disparando)
        {
            tiempoPresionado += Time.deltaTime;
            if (tiempoPresionado > TiempoMaximoPresionado)
            {
                CalentarArma();
            }
        }
        else
        {
            EnfriarArma();
        }

        if (heatBar.CurrentTemperature >= heatBar.MaxTemperature)
        {
            armaBloqueada = true;
            disparando = false;
            if (audioSource.isPlaying)
            {
                StartCoroutine(FadeOut(audioSource, fadeOutDuration)); // Iniciar el fade out
            }
        }
        else
        {
            armaBloqueada = false;
            StartCoroutine(Enfriando());
        }
    }

    IEnumerator DisparoContinuo()
    {
        while (disparando)
        {
            Disparar();
            yield return new WaitForSeconds(CadenciaDeDisparo);
        }
    }

    void Disparar()
    {
        GameObject bala = Instantiate(FuegoPrefab, PuntoDeDisparo.position, PuntoDeDisparo.rotation);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        rb.AddForce(PuntoDeDisparo.forward * VelocidadBala);
        Destroy(bala, 5f); // Destruir la bala después de 5 segundos    
        heatBar.SetAmountToUpdate(cantidadCalorDisparo * tiempoPresionado);
    }

    void CalentarArma()
    {
        if (!heatBar.armaBloqueada)
        {
            Debug.Log("Calentando el arma...");
            StartCoroutine(Calentando());
        }
    }

    void EnfriarArma()
    {
        StartCoroutine(Enfriando());
    }

    IEnumerator Calentando()
    {
        heatBar.armaBloqueada = true;
        while (disparando && heatBar.CurrentTemperature < heatBar.MaxTemperature)
        {
            heatBar.CurrentTemperature += Time.deltaTime * (cantidadCalorDisparo * tiempoPresionado);
            heatBar.HeatSlider.value = heatBar.CurrentTemperature;
            yield return null;
        }

        if (!disparando)
        {
            StartCoroutine(Enfriando());
        }
    }

    IEnumerator Enfriando()
    {
        while (heatBar.CurrentTemperature > 0)
        {
            heatBar.CurrentTemperature -= Time.deltaTime * heatBar.CoolingRate;
            heatBar.HeatSlider.value = heatBar.CurrentTemperature;
            yield return null;
        }

        if (heatBar.CurrentTemperature <= 0)
        {
            heatBar.CurrentTemperature = 0;
            heatBar.HeatSlider.value = heatBar.CurrentTemperature;
            heatBar.armaBloqueada = false; // Desbloquear el arma después de enfriar por completo    
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
