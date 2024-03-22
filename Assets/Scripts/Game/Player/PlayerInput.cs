using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private EllipticalMotion_XZPlane ellipticalMotion; // Reference to EllipticalScript

    private PlayerControls controls;
    private bool cannonIsReady = true;
    [SerializeField, Range(0f,30f)]
    private float fireRate=10f;
    [SerializeField]
    PoolController pool;
    private bool fireCoroutineRunning = false;

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.Movement.performed += OnMovePerformed;
        controls.Player.Movement.canceled += OnMoveCanceled;
       
    }

    private void StartFire()
    {
        if (cannonIsReady == true)
        {
            pool.SpawnBullet();
            cannonIsReady=false;
        }
        if(cannonIsReady == false && fireCoroutineRunning == true)
        {
            Debug.Log("Il cannone è scarico!!!");
        }
         if (!cannonIsReady && !fireCoroutineRunning)
        {
            Timing.RunCoroutine(loadingCannon());
        }
    }
    protected IEnumerator<float> loadingCannon()
    {
        fireCoroutineRunning = true;
        while (true)
        {
            yield return Timing.WaitForSeconds(fireRate);
            cannonIsReady = true;
            Debug.Log("Il cannone è carico!!!");
            StopCoroutine(loadingCannon());
        }
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
        controls.Player.Fire.performed += ctx => StartFire();



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