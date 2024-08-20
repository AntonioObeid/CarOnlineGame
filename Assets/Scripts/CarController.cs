using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class CarController : NetworkBehaviour
{
    public float moveSpeed = 50f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found!");
        }
    }

    void FixedUpdate()
    {
        if (IsOwner)
        {
            HandleMovementInput();
        }
    }

    void HandleMovementInput()
    {
        float moveDirection = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float turnDirection = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        SubmitMovementServerRpc(moveDirection, turnDirection);
    }

    [ServerRpc]
    void SubmitMovementServerRpc(float moveDirection, float turnDirection)
    {
        ApplyMovement(moveDirection, turnDirection);
    }

    void ApplyMovement(float moveDirection, float turnDirection)
    {
        rb.AddForce(transform.forward * moveDirection, ForceMode.VelocityChange);
        transform.Rotate(Vector3.up, turnDirection);
    }
}
