using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update() {

        // Get the input vector from the gameinput object class
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Convert to 3D space and 
        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

        // Collision Detection using Raycast / Capsule Cast
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Logic to handle sliding against walls
        if (!canMove) {
            // Can't move towards moveDir
            // Attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0.0f, 0.0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove) {
                // Can move only on the X direction
                moveDir = moveDirX;
            } else {
                // Can't move only on the X direction
                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0.0f, 0.0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove) {
                    // Can move only on the Z direction
                    moveDir = moveDirZ;
                }
            }
        }

        // Translation
        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        // Set isWalking if actually moving
        isWalking = moveDir != Vector3.zero;

        // Rotation including interpolation
        float rotateSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

    }

    public bool IsWalking() {
        return isWalking;
    }
}
