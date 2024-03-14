using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;

    Vector3 movementDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;

    public float moveSpeed = 7;
    public float rotspeed = 15;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovements()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        movementDirection = cameraObject.forward * inputManager.verticalInput;
        movementDirection=movementDirection+cameraObject.right * inputManager.horizontalInput;
        movementDirection.Normalize();
        movementDirection.y = 0;
        movementDirection = movementDirection * moveSpeed;

        Vector3 movementVelocity = movementDirection;
        playerRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection=cameraObject.forward * inputManager.verticalInput;
        targetDirection=targetDirection+cameraObject.right*inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation=Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation=Quaternion.Slerp(transform.rotation,targetRotation,rotspeed*Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
