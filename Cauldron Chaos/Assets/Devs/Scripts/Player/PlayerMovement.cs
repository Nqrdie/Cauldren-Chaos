using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    public CauldronChaos input;

    private Vector3 moveVector = Vector3.zero;

    private Rigidbody rb;
    private bool inRange;
    private GameObject enemy;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float pushStrength;
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] float stickToGroundForce = 10f;

    [SerializeField] float maxVelocity = 5f;

    [SerializeField] AudioClip punchSound;
    [SerializeField] AudioClip walkSound;

    [SerializeField] private int playerIndex = 0;

    private void Awake()
    {
        input = new CauldronChaos();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector3 inputVector)
    {
        moveVector = inputVector;
        moveVector.Normalize();
    }

    private void FixedUpdate()
    {
        if (IsGrounded(out Vector3 groundNormal))
        {
            Vector3 moveDirection = Vector3.ProjectOnPlane(moveVector, groundNormal).normalized;
            rb.velocity += moveDirection * speed * Time.fixedDeltaTime;
            rb.AddForce(-groundNormal * stickToGroundForce, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
        else
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    private void Update()
    {
        if (moveVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private bool IsGrounded(out Vector3 groundNormal)
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer))
        {
            groundNormal = hit.normal;
            return true;
        }

        groundNormal = Vector3.up;
        return false;
    }
}



//private void OnTriggerStay(Collider other)
//{
//    if (other.CompareTag("Player"))
//    {
//        if (other == other.gameObject.GetComponent<CapsuleCollider>())
//        {
//            inRange = true;
//            enemy = other.gameObject;
//        }
//    }
//}

//private void OnTriggerExit(Collider other)
//{
//    inRange = false;
//}