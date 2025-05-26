using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Finish Panel")]
    public GameObject resultPanel;
    public TMP_Text resultText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        resultPanel.SetActive(false);
    }

    public void ShowFinishResults(List<string> order)
    {
        resultPanel.SetActive(true);

        string result = " Yarış Sonuçları:\n";
        for (int i = 0; i < order.Count; i++)
        {
            result += $"{i + 1}. {order[i]}\n";
        }

        resultText.text = result;
    }
}
