using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LuLib.Transform;
using LuLib.Vector;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    // constants
    private Vector3 inputRotation;
    private float screenToWorldYFactor;

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

        // init variables
        inputRotation = mainCam!.transform.eulerAngles;
        screenToWorldYFactor = 1 / Mathf.Sin(inputRotation.y * Mathf.Deg2Rad);
    }

    private void FixedUpdate()
    {
        // calculate movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed; // input
        movement.Limit(speed);
        movement.Rotate(0, inputRotation.y, 0);

        // apply movement
        characterController.Move(movement * Time.fixedDeltaTime);

        // calculate rotation
        Vector3 delta = Input.mousePosition - mainCam.WorldToScreenPoint(transform.position); // input

        delta.y *= screenToWorldYFactor;

        float rotation = -((Vector2)delta).GetRotation();

        // apply rotation
        transform.SetAngleY(rotation + inputRotation.y);
    }
}
