using System;
using System.Collections;
using System.Collections.Generic;
using LuLib.Transform;
using LuLib.Vector;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{

    // constants
    private readonly Vector3 inputRotation = new(0, 45, 0);

    // inspector settings
    [SerializeField] private float speed;

    // references
    private CharacterController characterController;
    private Camera mainCam;
    
    private void Awake()
    {
        // get references
        characterController = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        // calculate movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed; // input
        movement.Limit(speed);
        movement.Rotate(inputRotation);

        // apply movement
        characterController.Move(movement * Time.fixedDeltaTime);

        // calculate rotation
        Vector2 delta = Input.mousePosition - mainCam.WorldToScreenPoint(transform.position);

        float rotation = -delta.GetRotation();

        // apply rotation
        transform.SetAngleY(rotation + inputRotation.y);
    }

    
}
