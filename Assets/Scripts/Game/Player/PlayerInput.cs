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
    public float raycastDistance;
    public LayerMask raycastMask;


    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.Movement.performed += OnMovePerformed;
        controls.Player.Movement.canceled += OnMoveCanceled;
        Timing.RunCoroutine(rayCastTarget());
        spawnCount = ammoCount;
    }

    protected IEnumerator<float> rayCastTarget()
    {
        while (isActiveAndEnabled)
        {
            Debug.DrawRay(transform.position, -transform.forward * raycastDistance, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, raycastDistance, raycastMask))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Enemy enemyComponent = hit.collider.GetComponent<Enemy>();
                    enemyComponent.ChangeColor();
                }

            }
            yield return Timing.WaitForOneFrame;
        }
        StopCoroutine("rayCastTarget");
    }


    private void StartFire()
    {
        if (cannonIsReady == true)
        {
            pool.SpawnBullet();
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
        }
    }
    protected  IEnumerator<float> loadingCannon()
    {
        cannonLoading = true;
        while (true)
        {
            yield return Timing.WaitForSeconds(reloadCannonTime);
            cannonIsReady = true;
            ammoCount = spawnCount;
            gc.UpdateAmmo(ammoCount);
            cannonLoading=false;
            break;
        }
        StopCoroutine("loadingCannon");
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