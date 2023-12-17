using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerActionsInput _input;

    private void Start()
    {
        _input = new PlayerActionsInput();
        _input.Player.Enable();
        _input.Player.DrivingState.performed += DrivingState_performed;

        _input.Player.ColorChange.performed += ColorChange_performed;
    }

    private void DrivingState_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _input.Player.Disable();
        _input.Driving.Enable();
    }

    private void ColorChange_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        this.gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }

    private void Update()
    {
        CalculateMovement();
        Drive();
        //RotateCube();

    }

    private void CalculateMovement()
    {
        //poll or check the input readings
        var move = _input.Player.Walk.ReadValue<Vector2>();
        transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * 5f);
        //transform.Translate(move * Time.deltaTime * 5f);
    }

    private void RotateCube()
    {
        Debug.Log(_input.Player.Rotate.ReadValue<float>());

        var rotateDirection = _input.Player.Rotate.ReadValue<float>();
        transform.Rotate(Vector3.up * Time.deltaTime * 30f * rotateDirection);
    }

    private void Drive()
    {
        var steer = _input.Driving.Steer.ReadValue<Vector2>();
        transform.Translate(new Vector3(steer.x, 0, steer.y) * Time.deltaTime * 5f);
    }

}

