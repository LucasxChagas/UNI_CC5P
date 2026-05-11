using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if(playerCollider.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Causar Dano ao Player
            playerCollider.GetComponent<Player>().TakeDamage(this.transform.position - playerCollider.transform.position);
        }
    }
}
