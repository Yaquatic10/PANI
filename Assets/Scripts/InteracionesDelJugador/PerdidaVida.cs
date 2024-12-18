using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdidaVida : MonoBehaviour
{
    public float BisAtk = 15;
    public float GaAtk = 30;
    public float MeAtk = 70;
    public float Muer = 150;
    public float backwardForce = .5f; // Fuerza con la que se moverá hacia atrás

    public Rigidbody rb;

    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody en el objeto.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SliVida sliVidaInstance = FindObjectOfType<SliVida>();

        if (sliVidaInstance == null)
        {
            Debug.LogError("No se encontró una instancia de SliVida.");
            return;
        }

        if (other.CompareTag("Enemigos"))
        {
            sliVidaInstance.PerdidaV(BisAtk); // Llamar al método PerdidaV en la instancia correcta
            MoveBackward();
        }
        else if (other.CompareTag("Enemigos2"))
        {
            sliVidaInstance.PerdidaV(GaAtk); // Llamar al método PerdidaV en la instancia correcta
            MoveBackward();
        }
        else if (other.CompareTag("Enemigos1"))
        {
            sliVidaInstance.PerdidaV(MeAtk); // Llamar al método PerdidaV en la instancia correcta
            MoveBackward();
        }
        else if (other.CompareTag("Muer"))
        {
            sliVidaInstance.PerdidaV(Muer); // Llamar al método PerdidaV en la instancia correcta
        }
        else if (other.CompareTag("BalaE"))
        {
            Debug.Log("Colisión con BalaE");
            sliVidaInstance.PerdidaV(GaAtk); // Llamar al método PerdidaV en la instancia correcta
            Destroy(other.gameObject); // Destruir la bala después de hacer
        }
    }

    private void MoveBackward()
    {
        if (rb != null)
        {
            // Aplicar una fuerza hacia atrás usando el Rigidbody
            rb.AddForce(-transform.right * backwardForce, ForceMode.Impulse);
        }
    }
}
