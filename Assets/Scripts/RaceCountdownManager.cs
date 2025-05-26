using TMPro;
using UnityEngine;
using System.Collections;

public class RaceCountdownManager : MonoBehaviour
{
    public static RaceCountdownManager Instance;
    public TextMeshProUGUI countdownText;

    public bool raceStarted = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        raceStarted = true;
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }
}
