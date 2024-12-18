using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Resivido : MonoBehaviour
{

    public Image esporasAliens;

    private float r;
    private float g;
    private float b;
    private float a;

    public SliVida vidaMaximaScript;
    public float coparacion = 30;

   
  
   

    void Start()
    {
        r = esporasAliens.color.r;
        g=esporasAliens.color.g;
        b=esporasAliens.color.b;
        a=esporasAliens.color.a;

     

        


    }

    // Update is called once per frame
    void Update()
    {

        float vidaActual = vidaMaximaScript.vidaMaxima;
        if (vidaActual < coparacion)
        {
            a += 0.01f;
        }
        else
        {
            a -= 0.001f;
        }
        a = Mathf.Clamp(a, 0, 1f);
        changeColor();
    }

    private void changeColor()
    {

        Color c= new Color(r,g,b,a);
        esporasAliens.color = c;



    }




}
