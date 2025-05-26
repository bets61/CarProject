using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRaceButton : MonoBehaviour
{
    public void StartRace()
    {
        if(GameManager.Instance == null)
        {
            Debug.LogError("GameManager is not found! ");
            return;
        }
        if(GameManager.Instance.selectedCarPrefab == null)
        {
            Debug.LogError("Car not selected!");
            return;
        }
        SceneManager.LoadScene("GameScene");
    }
        
    
}
