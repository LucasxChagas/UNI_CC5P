using UnityEngine;

public class EnemyHurtArea : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    public void TakeDamage()
    {
        Destroy(enemyController.gameObject);
    }
}
