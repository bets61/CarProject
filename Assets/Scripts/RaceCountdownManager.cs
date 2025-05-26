using System.Collections;
using UnityEngine;

public class RaceCountdownManager : MonoBehaviour
{
    public static RaceCountdownManager Instance;
    public bool raceStarted = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(CountdownAndStart());
    }

    IEnumerator CountdownAndStart()
    {
        Debug.Log("3...");
        yield return new WaitForSeconds(1f);
        Debug.Log("2...");
        yield return new WaitForSeconds(1f);
        Debug.Log("1...");
        yield return new WaitForSeconds(1f);
        Debug.Log("GO!");

        raceStarted = true;
    }
}
