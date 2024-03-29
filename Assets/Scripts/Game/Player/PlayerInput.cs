using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerMovement
{
    private PlayerControls controls;
    [SerializeField, Range(0f,30f)]
    private float reloadCannonTime;
    [SerializeField, Range(0f, 10f)]
    private float fireRatio;
    private bool cannonLoading = false;
    private int ammoCount=3;
    [SerializeField]
    GameController gc;
    [SerializeField]
    PoolController pool;
    public float raycastDistance;
    public LayerMask raycastMask;
    public Material highlightMaterial;
    public Material originalMaterial;
    private Renderer render;
    private Enemy lastHitEnemy;
    private bool IsHit=false;
    private bool canFire = true;
    private float reload;

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.Movement.performed += OnMovePerformed;
        controls.Player.Movement.canceled += OnMoveCanceled;
        Timing.RunCoroutine(rayCastTarget().CancelWith(gameObject));
    }

   


    private void StartFire()
    {
        if (ammoCount!=0 && canFire)
        {
            pool.SpawnBullet();
            ammoCount--;
            reload=reloadCannonTime;
            Timing.RunCoroutine(FireRatio(fireRatio).CancelWith(gameObject));
            gc.UpdateAmmo(ammoCount);
            if (ammoCount < 3 && !cannonLoading)
            {
             Timing.RunCoroutine(loadingCannon().CancelWith(gameObject));
            }
        }
    }
    protected  IEnumerator<float> loadingCannon()
    {
        cannonLoading = true;
        while (ammoCount<3)
        {
            yield return Timing.WaitForSeconds(reload);
            ammoCount++;
            gc.UpdateAmmo(ammoCount);
        }
        cannonLoading = false;
        StopCoroutine("loadingCannon");
    }

    protected IEnumerator<float> FireRatio(float fireRatio)
    {
        while (fireRatio > 0)
        {
            canFire = false;
            fireRatio--;
            yield return Timing.WaitForSeconds(1f);
        }
        canFire = true;
        StopCoroutine("FireRatio");
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
        SetMovementDirection(moveDirection);
    }


    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // When button is Unpressed stopMovement
        SetMovementDirection(Vector3.zero);
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
                    if (enemyComponent != null)
                    {
                        lastHitEnemy = enemyComponent;
                        render = enemyComponent.GetComponentInChildren<MeshRenderer>();
                        render.material = highlightMaterial;
                        IsHit = true;
                    }
                }

            }
            else
            {
                if (IsHit == true)
                {
                    render = lastHitEnemy.GetComponentInChildren<MeshRenderer>();
                    render.material = originalMaterial;
                    lastHitEnemy = null; // Azzera l'ultimo nemico colpito
                    IsHit = false;
                }

            }
            yield return Timing.WaitForOneFrame;
        }
        StopCoroutine("rayCastTarget");
    }

}