using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger’a giren nesne: {other.name}");

        CarIdentifier car = other.GetComponentInParent<CarIdentifier>();
        if (car == null)
        {
            Debug.LogWarning(" CarIdentifier component'i bulunamadı!");
            return;
        }

        if (car.hasFinished) return;

        car.hasFinished = true;
        RaceManager.Instance.RegisterFinish(car.carName, car.isPlayer);
    }
}
