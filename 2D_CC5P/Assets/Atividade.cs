using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Atividade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_InputField inputField;

    [Header("Botões Coloridos")]
    [SerializeField] private Button redButton;
    [SerializeField] private Button greenButton;
    [SerializeField] private Button blueButton;
    [SerializeField] private Color[] buttonColors;

    void Start()
    {
        confirmButton.onClick.AddListener(DisplayText);

        redButton.onClick.AddListener(() => SetTextColor(0));
        greenButton.onClick.AddListener(() => SetTextColor(1));
        blueButton.onClick.AddListener(() => SetTextColor(2));
    }

    public void DisplayText()
    {
        string currentText = inputField.text;
        resultText.text = currentText;

        if(string.IsNullOrEmpty(resultText.text))
        {
            resultText.text = "Campo Vazio!";
        }
    }

    public void SetTextColor(int index)
    {
        resultText.color = buttonColors[index];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
