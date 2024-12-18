using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float calor = 13;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigos"))
        {
            IA IAInstance = FindObjectOfType<IA>(); 
            if (IAInstance != null)
            { 
                IAInstance.Dano(calor); 
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Enemigos2"))
        {
            IAG IAGInstance = FindObjectOfType<IAG>();
            if (IAGInstance != null)
            {
                IAGInstance.DanoG(calor);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Enemigos1"))
        {  IAM IAMInstance = FindObjectOfType<IAM>();
            if (IAMInstance != null)
            {
                IAMInstance.DanoM(calor);
                Destroy(gameObject);
            }
        }
           

    }
}
