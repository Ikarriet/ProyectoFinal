using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    TirarCana tirarCana;

    Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;
    private void OnEnable()
    {
        tirarCana = GetComponent<TirarCana>();
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Move.performed += i => movementInput = i.ReadValue<Vector2>();

            
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMoveInput();
    }
    private void HandleMoveInput()
    {
        verticalInput=movementInput.y;
        horizontalInput=movementInput.x;        
    }
}
