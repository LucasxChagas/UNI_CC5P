using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;
    public TMP_Text pointsText;
    private int points;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddPoints(int newPoints)
    {
        points += newPoints;
        pointsText.text = $"Moedas: {points}";
    }
}
