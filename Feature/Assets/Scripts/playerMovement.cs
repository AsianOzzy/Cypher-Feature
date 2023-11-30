using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    PlayerInputAction playerActions;

    private Rigidbody body;

    public float moveSpeed = 20f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        playerActions = new PlayerInputAction();

        playerActions.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = playerActions.playerInGameAction.Move.ReadValue<Vector2>();
        GetComponent<Rigidbody>().transform.position += new Vector3(moveVec.x * moveSpeed * Time.deltaTime, moveVec.y * moveSpeed * Time.deltaTime);
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
