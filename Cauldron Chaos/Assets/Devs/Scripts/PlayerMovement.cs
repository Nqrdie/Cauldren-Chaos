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
    private PConfig playerConfig;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float pushStrength;
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] float stickToGroundForce = 10f;

    [SerializeField] AudioClip punchSound;
    [SerializeField] AudioClip walkSound;

    private void Awake()
    {
        input = new CauldronChaos();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;  
    }

    public void Push_Performed(InputAction.CallbackContext value)
    {
        if (inRange && enemy != null)
        {
            if (enemy.TryGetComponent<Rigidbody>(out Rigidbody enemyRb))
            {
                enemyRb.AddForce(transform.forward * pushStrength, ForceMode.Impulse);
                enemy.transform.LookAt(transform.position);
                enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
                AudioManager.instance.PlaySound(punchSound);
            }
        }
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
        }
        else
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
    }

    public void Move()
    {
        if (IsGrounded(out Vector3 groundNormal))
        {
            Vector3 moveDirection = Vector3.ProjectOnPlane(moveVector, groundNormal).normalized;
            rb.velocity += moveDirection * speed * Time.fixedDeltaTime;


            rb.AddForce(-groundNormal * stickToGroundForce, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);
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

    private void OnTriggerEnter(Collider other)
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