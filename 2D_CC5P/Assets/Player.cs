using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 movementInputs;
    [SerializeField] private float movementSpeed;
    [SerializeField] SpriteRenderer enemy;
    private Rigidbody2D rb;

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
        // Movimento por Translate
        //transform.Translate(new Vector3(movementInputs.x, movementInputs.y, 0).normalized *
        //    Time.deltaTime * movementSpeed);

        // Movimento por Velocity (rigidBody)
        rb.linearVelocity = movementInputs * movementSpeed;

        //rb.AddForce(movementInputs * movementSpeed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Switch"))
        {
            Debug.Log("Começou a Colisão");
            enemy.color = Color.black;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Switch"))
        {
            Debug.Log("Permanece na Colisão");

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Switch"))
        {
            Debug.Log("Saiu da Colisão");
            enemy.color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Saiu da Colisão");
            enemy.color = Color.yellow;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Saiu da Colisão");
            enemy.color = Color.red;
        }
    }

}
