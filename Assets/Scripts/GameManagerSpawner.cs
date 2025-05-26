using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpawner : MonoBehaviour
{
    public GameObject gameManagerPrefab;
    private void OnEnable()
    {
        if(GameManager.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }
    }
}
