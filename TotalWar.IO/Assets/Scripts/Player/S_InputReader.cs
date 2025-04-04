using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputReader : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool AllowMovement { get; private set; }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void AllowMovements(InputAction.CallbackContext context) 
    {
        AllowMovement = context.ReadValueAsButton();
    }
}
