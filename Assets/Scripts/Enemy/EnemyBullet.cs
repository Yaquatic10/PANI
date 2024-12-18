using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{





    private void Start()
    {
        
        Destroy(gameObject, 5);
    }

   

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            
            
            
          
          
        }


    }*/
}
