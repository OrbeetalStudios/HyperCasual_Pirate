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
    private float reloadCannonTime=5f;
    private bool cannonLoading = false;
    [SerializeField, Range(0f,10)]
    private int ammoCount;
    private int spawnCount;
    [SerializeField]
    GameController gc;
    [SerializeField]
    PoolController pool;


    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.Movement.performed += OnMovePerformed;
        controls.Player.Movement.canceled += OnMoveCanceled;
        spawnCount = ammoCount;
    }

    private void StartFire()
    {
        if (cannonIsReady == true)
        {
            pool.SpawnBullet(spawnCount);
            ammoCount--;
            gc.UpdateAmmo(ammoCount);
            if (ammoCount <= 0)
            {
                cannonIsReady = false;
                if (cannonLoading == false)
                {
                    Timing.RunCoroutine(loadingCannon());
                }
            }
            else if (cannonLoading == true)
            {
                Debug.Log("Cannon is empty!!!");
            }
        }
    }
    protected  IEnumerator<float> loadingCannon()
    {
        cannonLoading = true;
        while (true)
        {
            Debug.Log("Reload Cannon for " + reloadCannonTime+" seconds");
            yield return Timing.WaitForSeconds(reloadCannonTime);
            cannonIsReady = true;
            Debug.Log("Cannon is READY!!!");
            ammoCount = spawnCount;
            gc.UpdateAmmo(ammoCount);
            cannonLoading=false;
            break;
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