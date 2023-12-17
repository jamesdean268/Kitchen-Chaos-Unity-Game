using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7.0f;
    private bool isWalking;

    private void Update() {

        // Get Input Vector from keyboard
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

        // Normalise vector so diagonal movement is clamped
        inputVector = inputVector.normalized;

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
