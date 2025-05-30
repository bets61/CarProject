using UnityEngine;

public class RaceStartManager : MonoBehaviour
{
    public Transform baseSpawnPoint;
    public float spacing = 2.6f; 
    public GameObject[] allCarsPrefabs;
    public Spline spline;

    private void Start()
    {
        SpawnAllCars();
    }

    private void SpawnAllCars()
    {
        int selectedIndex = GameManager.Instance.selectedCarIndex;
        int totalCars = allCarsPrefabs.Length;
        float startT = 0.005f;

        for (int i = 0; i < totalCars; i++)
        {
            float halfStepFix = (totalCars % 2 == 0) ? 0.5f : 0f;
            float xOffset = (i - totalCars / 2f + halfStepFix) * spacing;

            Vector3 splineStart = spline.GetPoint(startT);
            Vector3 forward = (spline.GetPoint(startT + 0.01f) - splineStart).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward);

            Vector3 spawnPos = splineStart + right/2f * xOffset;
            spawnPos.y += 1f;

            GameObject car = Instantiate(allCarsPrefabs[i], spawnPos, Quaternion.LookRotation(forward));
            car.name = $"Car_{i}";

            Rigidbody rb = car.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }

            var id = car.AddComponent<CarIdentifier>();
            id.isPlayer = (i == selectedIndex);
            id.carName = id.isPlayer ? "Sen" : $"Bot {i + 1}";

            if (id.isPlayer)
            {
                var controller = car.AddComponent<CarController>();
                controller.isPlayerControlled = true;
                controller.SetupWheels(car);
                Camera.main.GetComponent<CameraFollow>().SetTarget(car.transform);
            }
            else
            {
                var bot = car.AddComponent<SplineBotController>();
                bot.spline = spline;
                bot.offset = xOffset/2f;
                bot.baseSpeed = Random.Range(6f, 25f);  
                bot.t = startT;
            }
        }
    }
}
