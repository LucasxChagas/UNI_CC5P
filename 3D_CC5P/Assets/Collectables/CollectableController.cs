using UnityEngine;

public class CollectableController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Adiciona pontos ao jogador
            PointsManager.Instance.AddPoints(1);

            // Destrói o coletável
            Destroy(gameObject);
        }
    }
}
