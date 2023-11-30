using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    PlayerInputAction playerActions;

    public float moveSpeed = 20f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        playerActions = new PlayerInputAction();

        playerActions.Enable();
    }

    private void FixedUpdate()
    {
        Vector3 moveVec = playerActions.playerInGameAction.Move.ReadValue<Vector2>();

        Vector3 movement = (moveVec.y * transform.forward) + (moveVec.x * transform.right);
        GetComponent<CharacterController>().Move(movement * moveSpeed * Time.deltaTime);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump! :" + context.phase);
        if (context.performed)
        {
            Debug.Log("Real Jump");
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
