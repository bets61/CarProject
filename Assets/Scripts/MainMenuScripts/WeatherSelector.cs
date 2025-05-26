using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSelector : MonoBehaviour
{
    public WeatherMode weatherMode;
    public void SelectWeather()
    {
        GameManager.Instance.selectedWeatherMode = weatherMode;
        Debug.Log("Selected weather: " + weatherMode);   
    }
}
