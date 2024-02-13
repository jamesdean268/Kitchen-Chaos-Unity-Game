using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }

    }

    private void Update() {

        HandleMovement();
        HandleInteractions();

    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {

        // Get the input vector from the gameinput object class
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // Convert to 3D space and 
        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        // Determine if we hit an interactable object and output it to raycastHit
        float interactDistance = 2.0f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // Has ClearCounter
                if (clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }

    }

    private void HandleMovement() {

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

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        // Fire the event when the selected counter changes, assign the custom variable as the selected counter
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
