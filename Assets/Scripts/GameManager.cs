
using UnityEngine;

public enum ControlMode { Keyboard, SteeringWheel }
public enum WeatherMode { Day, Night, Rainy }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject selectedCarPrefab;
    public ControlMode selectedControlMode;
    public WeatherMode selectedWeatherMode;
    
    public int selectedCarIndex = -1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;    
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
