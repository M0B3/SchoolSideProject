using UnityEngine;

[RequireComponent(typeof(S_InputReader))]
public class S_PlayerMovements : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool lockCursor = true;
    [SerializeField, Range(0.1f, 10f)] private float cameraSensitivity = 5f;

    //-- Private Components --//
    private S_InputReader inputReader;
    private Transform camTransform;


    private void Awake()
    {
        //-- Get All Components --//
        inputReader = GetComponent<S_InputReader>();
        camTransform = Camera.main.transform;
    }
    private void Start()
    {
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
        camTransform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10);
    }
}
