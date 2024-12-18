using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntos : MonoBehaviour
{
    public float puntos;
    public TextMeshProUGUI textMesh1;
    public TextMeshProUGUI textMesh2;

    private void Start()
    {
        // Si no se ha asignado un TextMeshProUGUI en el Inspector, intenta obtenerlo del GameObject actual
        if (textMesh1 == null)
        {
            textMesh1 = GetComponent<TextMeshProUGUI>();
        }

        if (textMesh2 == null)
        {
            // Suponiendo que el segundo TextMeshProUGUI esté en otro GameObject, cambia 'GetComponent' a 'FindObjectOfType'
            textMesh2 = FindObjectOfType<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        // Actualiza el texto con los puntos en ambos TextMeshProUGUI
        textMesh1.text = puntos.ToString("0");
        textMesh2.text = puntos.ToString("0");
    }

    // Método para sumar puntos
    public void SumarPuntos(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }
}
