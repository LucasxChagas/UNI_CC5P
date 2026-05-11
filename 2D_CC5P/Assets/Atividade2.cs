using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Atividade2 : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        // guarda a referência no Start, nunca no Update
        audioSource.Play();
    }
}
