using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    // Update is called once per frame
    void Update(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalised();

        // Move in the direction of input vector
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Walking var for animator
        isWalking = moveDir != Vector3.zero;

        // For rotating the Character into the direction of movement
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        // Debug.Log(inputVector);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
