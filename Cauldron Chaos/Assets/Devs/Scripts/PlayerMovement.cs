using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CauldronChaos input;

    private Vector3 moveVector = Vector3.zero;

    private Rigidbody rb;
    private bool inRange;
    private GameObject enemy;

    

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float pushStrength;

    private void Awake()
    {
       input = new CauldronChaos();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += Move_performed;
        input.Player.Move.canceled += Move_canceled;
        input.Player.Fire.performed += Push_Performed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= Move_performed;
        input.Player.Move.canceled -= Move_canceled;
        input.Player.Fire.performed -= Push_Performed;
    }


    public void Push_Performed(InputAction.CallbackContext value)
    {
        if (inRange && enemy != null)
        {
            if (enemy.TryGetComponent<Rigidbody>(out Rigidbody enemyRb))
            {
                enemyRb.AddForce(transform.forward * pushStrength, ForceMode.Impulse);
                enemy.transform.LookAt(transform.position);
            }
        }
    }

    public void Move_canceled(InputAction.CallbackContext value)
    {
        moveVector = Vector3.zero;
    }

    public void Move_performed(InputAction.CallbackContext value)
    {
        moveVector = new Vector3(value.ReadValue<Vector3>().x, 0, value.ReadValue<Vector3>().z);
        moveVector.Normalize();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * speed * Time.fixedDeltaTime * 100;
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
        if(other.CompareTag("Player"))
        {   
            if (other = other.gameObject.GetComponent<CapsuleCollider>())
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
