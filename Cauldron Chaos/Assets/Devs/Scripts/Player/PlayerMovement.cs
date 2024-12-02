using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public CauldronChaos input;

    public UnityEvent<GameObject> onPlayerDeath;

    private Vector3 moveVector = Vector3.zero;

    private Rigidbody rb;
    private bool inRange;
    private GameObject enemy;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float pushCooldownTime;
    [SerializeField] bool pushCooldown;
    [SerializeField] float pushStrength;
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] float stickToGroundForce = 10f;

    [SerializeField] float maxVelocity = 5f;

    [SerializeField] AudioClip punchSound;
    [SerializeField] AudioClip walkSound;

    [SerializeField] private int playerIndex = 0;

    [SerializeField] AudioSource _audioSource;

    private bool pushed;

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
    public void Push()
    {
        if (inRange && enemy != null && enemy != this.gameObject && !pushCooldown && !pushed)
        {
            StartCoroutine(Cooldown());
            animator.SetBool("Pushing", true);
            AudioManager.instance.PlaySound(punchSound);
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            enemy.transform.rotation.SetLookRotation(transform.position - enemy.transform.position);
            enemy.GetComponent<Rigidbody>().AddForce(direction * pushStrength, ForceMode.Impulse);

            PlayerMovement enemyMovement = enemy.GetComponent<PlayerMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.StartCoroutine(enemyMovement.Pushed());
            }
        }
    }

    public IEnumerator Pushed()
    {
        pushed = true;
        yield return new WaitForSeconds(2.25f);
        pushed = false;
    }

    private IEnumerator Cooldown()
    {
        pushCooldown = true;
        yield return new WaitForSeconds (1.125f);
        animator.SetBool("Pushing", false);
        yield return new WaitForSeconds(pushCooldownTime - 1.125f);
        pushCooldown = false;
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
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        if (!pushed)
        {
            if (IsGrounded(out Vector3 groundNormal))
            {
                Vector3 moveDirection = Vector3.ProjectOnPlane(moveVector, groundNormal).normalized;
                rb.velocity += moveDirection * speed * Time.fixedDeltaTime;
                rb.AddForce(-groundNormal * stickToGroundForce, ForceMode.Acceleration);
                if (moveDirection ==  Vector3.zero)
                {
                    _audioSource.Play();
                }
            }
            else
            {
                rb.AddForce(Physics.gravity, ForceMode.Acceleration);
            }
        }
        if(!IsGrounded(out Vector3 ground))
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        if (moveVector != Vector3.zero)
        {
            
            Quaternion toRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            if (!pushed)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        animator.SetBool("Pushed", pushed);
        if(moveVector != Vector3.zero)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
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


