using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private bool isWalking;
    private Vector3 lastInteractDir;


    // Update is called once per frame
    void Update(){
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteraction() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalised();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // Has ClearCounter
                clearCounter.interact();
            }

            /*
            // Functionality of TryGetComponent
            ClearCounter clearCounter1 = raycastHit.transform.GetComponent<ClearCounter>(); 
            if(clearCounter1 != null) {
                // Has ClearCounter
            }*/
        }
        else {
            // Debug.Log("-");
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalised();

        // Move in the direction of input vector
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Collision Detection With Physics.Raycast
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        /* bool canMove = !Physics.Raycast(transform.position, moveDir, playerRadius); // Raycast casues problem on the edges */
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Handle Diagonal Movement
        if (!canMove) {
            // Cannot move towards moveDir
            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; // Normalization helps to keep spped same as side even on diagonal movement
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove) {
                // Can Move only in X
                moveDir = moveDirX;
            } else {
                // Cannot move in any direction
            }
        }
        // Straight movement
        if (canMove) {
            transform.position += moveDir * moveDistance;
        }


        // Walking var for animator
        isWalking = moveDir != Vector3.zero;

        // For rotating the Character into the direction of movement
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        // Debug.Log(inputVector);
    }
}
