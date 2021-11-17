using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public float moveSpeed = 10f; //Valor de la velocidad de movimiento del jugador en los ejes X y Z

    private float gravity = -40.81f; //Valor de la gravedad

    public float sphereRadius = 0.3f;

    public float jumpHeight;

    public float fuel = 100f;

    public float fuelLoss = 80f;

    public float fuelRecover = 40f;

    bool isGrounded;

    Vector3 playerVelocity; //Vector3 que posee las velocidades del jugador

    public CharacterController characterController; //CharacterController del jugador
    
    public Transform groundCheck;

    public LayerMask groundMask;
   
    void Update()
    {
        Jumping();
        Movement();   
        Gravity();
        GroundCheck();
        Walk();
        JetPack();
        Debug.Log(fuel);
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal"); //Obtiene el Input que se haga con el teclado en las teclas A y D
            
        float z = Input.GetAxis("Vertical"); //Obtiene el Input que se haga con el teclado en las teclas W y S

        Vector3 move = transform.right * x + transform.forward * z; /*Se crea un Vector3 que es igual a transform.right que puede manipular la posición de un 
                                                                     *  GameObject en el eje X  y transform.forward puede manipular la posición en el eje Z, 
                                                                     *  estas son multiplicadas por lo que obtienen las variables x y z */

        characterController.Move(move * moveSpeed * Time.deltaTime); //El CharacterController efectúa .Move que pide un Vector3, le introducimos el Vector3 move 
    }
    
    void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }
    }

    void Gravity()
    {
        playerVelocity.y += gravity * Time.deltaTime; //La velocidad del jugador en Y es igual a la misma sumada por la gravedad y mutliplicada por Time.deltaTime

        characterController.Move(playerVelocity * Time.deltaTime); //El CharacterController efectúa .Move que pide un Vector3, le introducimos el Vector3
    }
    
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask); /*isGrounded será true siempre y cuando el Vector3 de .CheckSphere esté siendo
                                                                                           *cumplido, la posición la toma apartir del groundCheck en los pies del jugador,
                                                                                           *la esfera tiene el radio de la variable sphereRadius y el groundMask tiene que ser
                                                                                           *equivalente a la layer que se le coloque, en este caso "Floor" */

        if (isGrounded && playerVelocity.y < 0)
        {

        playerVelocity.y = -2f; /*Si isGrounded es true la velocidad se mantiene en -2f, esto para que la gravedad esta medianamente inicializada y no comience a hacer el 
                                 * efecto de gravedad con tanto retraso*/
        }
    }

    void Walk()
    {
        if (Input.GetKey(KeyCode.LeftShift)) //Esta parte bastante explicativo
        {

        moveSpeed = 4f;

        }
  
        else
        {

        moveSpeed = 10f;

        }

    }

    void JetPack()
    {
        if (isGrounded==false && Input.GetKey(KeyCode.Space))
        {
            if (fuel >= 20f)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

                fuel -= fuelLoss * Time.deltaTime;
            }
        }
        else if (isGrounded==true)
        {
            fuel += fuelRecover * Time.deltaTime;
        }
        if (fuel>=100)
        {
            fuel = 100f;
        }
    }
}
