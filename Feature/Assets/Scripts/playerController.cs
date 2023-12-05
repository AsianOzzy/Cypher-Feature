using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class playerController : MonoBehaviour
{
    PlayerInputAction playerActions;

    public float moveSpeed = 10f;

    public GameObject cypherArm;
    public GameObject cypherCamera;

    private RaycastHit hit;

    private Vector3 pos;

    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cypherArm.SetActive(false);
    }

    private void Update()
    {
        if (cypherCamera != null)
        {
            cypherCamera.transform.position = pos;
        }


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

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadDefaultValue());
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            pos = hit.point;
        }
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

    public void Equip(InputAction.CallbackContext context)
    {
        Debug.Log("Camera Equipped :" + context.phase);
        if (context.performed)
        {
            cypherArm.SetActive(true);
        }

        Debug.Log("Camera Hovering");
        cypherCamera = Instantiate(cypherCamera, pos, transform.rotation);

    }

    public void placeCam(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cypherCamera = null;
            cypherArm.SetActive(false);
        }
    }

    private void pickUp(InputAction.CallbackContext context)
    {

    }
}
