using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed; // Vaikščiojimo greitis
    [SerializeField] private float runSpeed; // Bėgimo greitis
    [SerializeField] private float crouchSpeed; // Pasilenkusio veikėjo ejimo greitis
    [SerializeField] private float crouchHeight; // Pasilenkusio veikėjo aukštis
    [SerializeField] private float jumpSpeed; // Šuolio greitis
    [SerializeField] private float gravityScale; // Gravitacijos koeficientas

    private float verticalVelocity; // Vertikalus greitis
    private float originalHeight; // Pradinis veikėjo aukštis
    private float currentSpeed; // Dabartinis greitis
    private bool isCrouching; // Ar žaidėjas pasilenkęs
    private CharacterController characterController; // Žaidėjo valdymo komponentas

    // Start() metodas vykdomas paleidus žaidimą
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Gauname CharacterController komponentą
        originalHeight = characterController.height; // Išsaugome pradinį aukštį

        // Spausdiname pradinį aukštį
        Debug.Log("Original Height: " + originalHeight);
    }

    // Update() metodas vykdomas kiekvieną kadro atnaujinimą
    void Update()
    {
        if (Time.timeScale == 0) // Jei žaidimo laikas sustabdytas
        { 
            return; // Išeiname iš metodo
        } 

        float _xAxis = Input.GetAxis("Horizontal"); // Horizontal ašies įvestis
        float _zAxis = Input.GetAxis("Vertical"); // Vertical ašies įvestis
        Vector3 _movementDirection = new Vector3(_xAxis, 0, _zAxis); // Sukuriame judėjimo kryptį

        // Nustatome dabartinį greitį pagal klaviatūros įvestį
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            currentSpeed = runSpeed; // Jei paspausta LeftShift, bėgimo greitis
        } 
        else if (isCrouching) 
        { 
            currentSpeed = crouchSpeed; // Jei žaidėjas pasilenkęs, pasilenkusio žaidėjo greitis
        }
        else 
        {
            currentSpeed = walkSpeed; // Priešingu atveju, vaikščiojimo greitis
        }

        // Transformuojame judėjimo kryptį pagal žaidėjo orientaciją ir greitį
        _movementDirection = transform.TransformDirection(_movementDirection) * currentSpeed;

        if (characterController.isGrounded) // Jei žaidėjas yra ant žemės
        { 
            verticalVelocity = -1f; // Nustatome neigiamą vertikalų greitį, kad žaidėjas nesvyruotų

            if (Input.GetKeyDown(KeyCode.Space)) // Jei paspaustas tarpo klavišas
            { 
                verticalVelocity = jumpSpeed; // Nustatome vertikalų greitį šuoliui
            } 

            // Jei paspausta Control klavišas, žaidėjas lenkiasi
            if (Input.GetKey(KeyCode.LeftControl)) 
            { 
                Crouch(); // Vykdome Crouch() metodą
            } 
            // Jei atleistas Control klavišas, žaidėjas atsistoja
            else if (Input.GetKeyUp(KeyCode.LeftControl)) 
            { 
                StandUp(); // Vykdome StandUp() metodą
            } 
        } 
        else // Jei žaidėjas ore
        {
            verticalVelocity -= gravityScale * Time.deltaTime; // Taikome gravitaciją
        }
        _movementDirection.y = verticalVelocity; // Nustatome vertikalų judėjimą
        characterController.Move(_movementDirection * Time.deltaTime); // Judiname žaidėją
    }

    // Metodas, kuris vykdomas, kai žaidėjas lenkiasi
    private void Crouch() 
    { 
        isCrouching = true; // Nustatome, kad žaidėjas lenkiasi
        characterController.height = crouchHeight; // Nustatome CharacterController aukštį
    }

    // Metodas, kuris vykdomas, kai žaidėjas atsistoja
    private void StandUp() 
    { 
        isCrouching = false; // Nustatome, kad žaidėjas nebe lenkiasi
        characterController.height = originalHeight; // Atstatome CharacterController aukštį
    } 
}
