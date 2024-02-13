using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {

        // ---------- Legacy Input System for reference -------------
        //// Get Input Vector from keyboard
        //Vector2 inputVector = new Vector2(0, 0);
        //if (Input.GetKey(KeyCode.W)) {
        //    inputVector.y += 1;
        //}
        //if (Input.GetKey(KeyCode.S)) {
        //    inputVector.y -= 1;
        //}
        //if (Input.GetKey(KeyCode.A)) {
        //    inputVector.x -= 1;
        //}
        //if (Input.GetKey(KeyCode.D)) {
        //    inputVector.x += 1;
        //}

        // New input system
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Normalise vector so diagonal movement is clamped
        inputVector = inputVector.normalized;

        return inputVector;
    }

}
