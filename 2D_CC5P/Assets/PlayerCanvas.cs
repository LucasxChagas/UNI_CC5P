using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    public static PlayerCanvas Instance { get; private set; }
    public TMP_Text damageText;
    public Animator feedbackAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damageValue)
    {
        damageText.text = damageValue.ToString();
        feedbackAnimator.StopPlayback();
        feedbackAnimator.Play("HitFeedback");
    }
}
