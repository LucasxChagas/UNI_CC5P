using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyController enemyController;

    public void SetEnemyBoneAttack()
    {
        Debug.Log("Disparou o ataque do inimigo");
        enemyController.InstantiateBone();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

}
