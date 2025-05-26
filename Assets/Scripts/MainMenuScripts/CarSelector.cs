using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public GameObject carPrefab;
    public int carIndex;
    public void SelectCar()
    {
        if(GameManager.Instance == null)
        {
            Debug.LogError("GameManager is not found, car not selected");
            return;
        }
        if(carPrefab == null)
        {
            Debug.LogError("Prefab not assigned");
            return;
        }   

        GameManager.Instance.selectedCarPrefab = carPrefab;
        GameManager.Instance.selectedCarIndex = carIndex;
        Debug.Log("Selected Car: " + carPrefab.name);
    }
}
