using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public
    public bool isAlive = true;
    public float runSpeed;
    public float horizontalSpeed;
    public Rigidbody rb;


    // Private
    private float horizontalInput;
    [SerializeField] private float jumpForce = 150;
    [SerializeField] private LayerMask groundMask;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerHeight = GetComponent<Collider>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 forwardMovement = transform.forward * runSpeed * Time.fixedDeltaTime;
            Vector3 horizontalMovement = transform.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement + horizontalMovement);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
}
