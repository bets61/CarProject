using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject resultPanel;
    public Text resultText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowResult(int place)
    {
        resultPanel.SetActive(true);
        resultText.text = $"Yarýþý {place}. sýrada bitirdin!";
    }
}
