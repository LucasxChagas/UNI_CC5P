using UnityEngine;

public class EnemyDamageArea : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(this.transform.position - collision.transform.position);
        }
    }
}
