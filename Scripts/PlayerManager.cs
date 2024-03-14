using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMovement _playerMovement;
    InputManager _inputManager;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _inputManager.HandleAllInputs();
    }
    private void FixedUpdate()
    {
        _playerMovement.HandleAllMovements();
    }
}
