using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
public class HotBar : MonoBehaviour
{
    public Slider SobreCaS;

    public float MaxTemperatura = 100;

    private float CurrentTemperatura;

    public float Temperatura = 0;

    private float Enfriamiento = 0.1f;
    private float RegenerateAmount = 2f;

    private float CalentamientoTime = 0.1f;




    void Start()
    {
        CurrentTemperatura = Temperatura;
        SobreCaS.maxValue = Temperatura;
        SobreCaS.value= Temperatura;





    }

 
}
