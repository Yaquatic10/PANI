using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    public Slider HeatSlider;
    public float MaxTemperature = 100;
    public float CurrentTemperature;
    public float Temperature = 0;
    public float CoolingRate = 0.1f;
    public float RegenerateAmount = 15f;
    public float HeatingTime = 0.005f;
    public bool armaBloqueada = false;
    public FuegoPantanoso CalentarArma;

    void Start()
    {
        CurrentTemperature = Temperature;
        HeatSlider.maxValue = MaxTemperature;
        HeatSlider.value = Temperature;
    }

    private float amountToUpdate;

    public void Update()
    {
        if (CurrentTemperature < MaxTemperature)
        {
            StartCoroutine(HeatingCoroutine());
        }
        else
        {
            Debug.Log("Sobrecalentamiento");
        
        }
    }

    public void SetAmountToUpdate(float amount)
    {
        amountToUpdate = amount;
    }

    private IEnumerator HeatingCoroutine()
    {
        while (CurrentTemperature < MaxTemperature)
        {
            CurrentTemperature += amountToUpdate;
            HeatSlider.value = CurrentTemperature;
            yield return new WaitForSeconds(HeatingTime);
        }
        HeatSlider.value = MaxTemperature; // Asegurar que el slider se establezca en el valor máximo
    }
}