using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMouse : MonoBehaviour
{

    // The camera's position in the game. Can be changed from 1st to 3rd person.
    [SerializeField] private Transform cameraPosition;
    public float mouseSensitivity = 100f;

    private PlayerMovement _playerMovement;

    float _rotation = 0; 

    float _xrotation = 0;

    [FormerlySerializedAs("Reverse")] public bool reverse;
    [FormerlySerializedAs("X_on_Y")] public bool xOnY;

    
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock our cursor on the screen
        cameraPosition = GameObject.Find("1st Person Camera position").transform;
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }


    void Update()
    {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * (mouseSensitivity * Time.deltaTime);

        _rotation -= mouse.y;
        // Rotation.y clamped for so the player doesn't do a backflip/frontfilp.
        _rotation = Math.Clamp(_rotation, -90f, 90f);
        _xrotation += mouse.x;

        transform.rotation = cameraPosition.rotation;
        transform.position = new Vector3(cameraPosition.transform.position.x, cameraPosition.transform.position.y, cameraPosition.transform.position.z );
        
        if(!_playerMovement.hasFallen) // Restrict camera movement when player is getting up.
            transform.localEulerAngles = new Vector3(_rotation, _xrotation, 0f);

        if (reverse) 
        {
            transform.localEulerAngles = new Vector3(-_rotation, -_xrotation, 0f); // Reverse rotaatio
        }

        if (xOnY) 
        {
            transform.localEulerAngles = new Vector3(_xrotation, _rotation, 0f); // X on Y ja Y on X
        }

    }
}
