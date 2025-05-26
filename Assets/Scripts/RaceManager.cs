using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    private List<string> finishOrder = new List<string>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterFinish(string carName, bool isPlayer)
    {
        if (finishOrder.Contains(carName)) return;

        finishOrder.Add(carName);
        Debug.Log($"{carName} yarýþý {finishOrder.Count}. sýrada bitirdi.");

        if (isPlayer)
        {
            UIManager.Instance.ShowFinishResults(finishOrder);
        }
    }
}
