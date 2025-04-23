using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 movementInputs;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;

    private bool IsWalking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateAnimator();
        UpdateOrientation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInputs()
    {
        movementInputs = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        rb.linearVelocity = movementInputs * movementSpeed;
    }

    private void UpdateAnimator()
    {
        if(rb.linearVelocity.magnitude > 0)
            IsWalking = true;
        else
            IsWalking = false;

        anim.SetBool("IsWalking", IsWalking);
    }

    private void UpdateOrientation()
    {
        if(movementInputs.x > 0) // Está indo para direita...
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(movementInputs.x < 0) // Está indo para esquerda...
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
