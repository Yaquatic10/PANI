using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Movimiento
    private Rigidbody rb;
    public float speed;

    // Camara
    public Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    void FixedUpdate() // Usa FixedUpdate para cálculos físicos
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

       Vector3 cameraForward= mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 movementDirection =  (cameraForward * ver + cameraRight * hor).normalized;
        rb.velocity = new Vector3(movementDirection.x * speed, rb.velocity.y, movementDirection.z * speed);
    }

    void Update()
    {
   
    }
}
