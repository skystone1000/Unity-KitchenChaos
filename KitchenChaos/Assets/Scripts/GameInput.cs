using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour{

    private PlayerInputAction playerInputAction;

    private void Awake() {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalised() {
        Vector2 inputVector = new Vector2(0, 0);

        // Error for UpDownLeftRightComposite
        // https://forum.unity.com/threads/no-option-of-2d-vector-composite-in-input-system-1-0-0.888607/

        // playerInputAction.Player.Move.ReadValue<Vector2>();


        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = +1;
        }

        // Debug.Log(inputVector);

        // Normalize Input to have same magnitude
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
