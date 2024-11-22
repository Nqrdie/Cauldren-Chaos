using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    public CauldronChaos input;

    public UnityEvent<GameObject> onPlayerDeath;

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
        onPlayerDeath = new UnityEvent<GameObject>();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnEnable()
    {
        input.Player.Fire.performed += Fire_performed;
    }

    private void OnDisable()
    {
        input.Player.Fire.canceled -= Fire_canceled;
    }
    private void Fire_performed(CallbackContext obj)
    {
        if(inRange)
        {
            Vector3 direction = transform.position - enemy.transform.position;
            enemy.GetComponent<Rigidbody>().AddForce(direction * pushStrength, ForceMode.Impulse);
        }
    }

    private void Fire_canceled(CallbackContext obj)
    {

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other == other.gameObject.GetComponent<CapsuleCollider>())
            {
                inRange = true;
                enemy = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}


