using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    // Valdymo greitis žaidėjo rotacijai
    [SerializeField] private float turnSpeed;
    // Minimalus kampas vertikalioje ašyje (žemyn)
    [SerializeField] private float minVerticalAngle;
    // Maksimalus kampas vertikalioje ašyje (aukštyn)
    [SerializeField] private float maxVerticalAngle;
    // Ar invertuoti pelės judėjimą (jei true, aukštyn/žemyn bus atvirkščiai)
    [SerializeField] private bool invertMouse;
    // Ar apriboti vertikalios rotacijos kampą
    [SerializeField] private bool clampVerticalRotation;
    // Žaidėjo kameros nuoroda
    [SerializeField] private Camera playerCamera;

    // Dabartinė vertikali rotacija pagal Y ašį (naudojama vertikalios kameros rotacijai sekti)
    private float currentRotationY;

    // Start funkcija paleidžiama, kai objektas inicijuojamas žaidime
    void Start()
    {
        // Paslepia žymeklį ekrane
        Cursor.visible = false; 
        // Užfiksuoja žymeklį ekrano viduryje, kad pelės judėjimas paveiktų tik žaidimo vaizdą
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update funkcija vykdoma kiekvieną kadrą (kiekvieną žaidimo atnaujinimą)
    void Update()
    {
        // Patikriname, ar žaidimas nėra pristabdytas (kai laiko skalė lygi 0)
        if (Time.timeScale == 0) 
        { 
            return; // Išėjimas iš funkcijos, jei žaidimas pristabdytas
        }

        // Paimame pelės judėjimą X ir Y ašyse ir padauginame iš turnSpeed, kad nustatytume rotacijos greitį
        float _mouseX = Input.GetAxis("Mouse X") * turnSpeed; 
        float _mouseY = Input.GetAxis("Mouse Y") * turnSpeed;

        // Jei invertMouse yra true, apverčiame pelės judėjimą Y ašyje
        if (invertMouse) 
        { 
            _mouseY *= -1; 
        }

        // Jei clampVerticalRotation yra true, apribojame vertikalią rotaciją tarp nustatytų kampų
        if (clampVerticalRotation)
        {
            // Apribojame vertikalios kameros rotaciją tarp minVerticalAngle ir maxVerticalAngle reikšmių
            currentRotationY = Mathf.Clamp(currentRotationY + _mouseY, minVerticalAngle, maxVerticalAngle);
        }
        else
        {
            // Leidžiame nevaržomą vertikalią rotaciją, nepritaikant jokių apribojimų
            currentRotationY += _mouseY;
        }

        // Sukame žaidėją horizontaliai pagal X ašį
        transform.Rotate(Vector3.up * _mouseX); 
        // Sukame kamerą vertikaliai pagal dabartinę vertikalią rotaciją Y ašyje
        playerCamera.transform.localRotation = Quaternion.Euler(Vector3.right * currentRotationY); 
    }
}