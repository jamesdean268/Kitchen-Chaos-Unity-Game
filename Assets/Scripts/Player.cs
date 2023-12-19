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

        // Convert to 3D space and set isWalking if actually moving
        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;

        // Translation
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Rotation including interpolation
        float rotateSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

    }

    public bool IsWalking() {
        return isWalking;
    }
}
