using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSensitivity; //Sensibilidad de la Camara

    public Transform playerBody; //El transform de la capsula (jugador)

    float xRotation = 0f; //Rotación de la camara en el eje Y (Transform)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lockea el cursor en el medio para que no se vea
    }

    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()   
    {
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity * Time.deltaTime; //Obtiene input del mouse en el eje X

        float mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity * Time.deltaTime; //Same pero en el eje Y

        xRotation -= mouseY;
 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Se Clampean los valores de la rotación en Y de manera que no sobrepase 90grados

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Se efectua la rotación en Y

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
