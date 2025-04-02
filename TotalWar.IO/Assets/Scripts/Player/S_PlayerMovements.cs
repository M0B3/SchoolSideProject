using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(S_InputReader))]
public class S_PlayerMovements : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool lockCursor = true;
    [SerializeField, Range(0.1f, 10f)] private float cameraSensitivity = 5f;
    [Header("References")]
    [SerializeField] private Transform camTransform;
    [SerializeField] private Transform selfTranform;
    [SerializeField] private S_InputReader inputReader;

    private void OnEnable()
    {
        if (!IsOwner) return;
        

        if (lockCursor) //-- Lock Cursor if wanted --//
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        CameraMovements();
    }

    private void Move()
    {
        //-- Get input + Move Camera --//
        Vector2 movementInput = inputReader.MovementInput;
        camTransform.position += new Vector3(movementInput.x, movementInput.y, 0) * Time.deltaTime * cameraSensitivity;
    }

    private void CameraMovements()
    {
        //-- Move Camera if allowed --//
        if (inputReader.AllowMovement)
        {
            Move();
        }

    }

    public void ResetCameraPosition()
    {
        //-- Camera go back to Spawn Position --//
        camTransform.position = new Vector3(selfTranform.position.x, selfTranform.position.y, -10);
    }
}
