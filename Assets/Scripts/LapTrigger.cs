using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CarIdentifier car = other.GetComponent<CarIdentifier>();
        if (car != null && !car.hasFinished)
        {
            car.hasFinished = true;
            RaceManager.Instance.RegisterFinish(car.carName, car.isPlayer);
        }
    }
}
