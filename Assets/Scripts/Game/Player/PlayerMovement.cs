using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EllipticalMotionController : MonoBehaviour
{
    [SerializeField] private EllipticalMotion_XZPlane ellipticalMotion; // Reference to EllipticalScript

    private PlayerControls controls;

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.Movement.performed += OnMovePerformed;
        controls.Player.Movement.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Movement.performed -= OnMovePerformed;
        controls.Player.Movement.canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        float horizontalInput = inputVector.x;
        float verticalInput = inputVector.y;

        // Direction UserInput
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Velocity of Input
        ellipticalMotion.SetMovementDirection(moveDirection);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // When button is Unpressed stopMovement
        ellipticalMotion.SetMovementDirection(Vector3.zero);
    }
}