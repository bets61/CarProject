using UnityEngine;

public class RaceStartManager : MonoBehaviour
{
    public Transform baseSpawnPoint;
    public float spacing = 4f;
    public GameObject[] allCarsPrefabs;
    public Spline spline;

    void Start()
    {
        SpawnAllCars();
    }

    void SpawnAllCars()
    {
        int selectedIndex = GameManager.Instance.selectedCarIndex;

        for (int i = 0; i < allCarsPrefabs.Length; i++)
        {
            float xOffset = (i - allCarsPrefabs.Length / 2f) * spacing;
            Vector3 spawnPos = baseSpawnPoint.position + baseSpawnPoint.right * xOffset;

            if (Physics.Raycast(spawnPos + Vector3.up * 3f, Vector3.down, out RaycastHit hit, 10f))
            {
                spawnPos = hit.point + Vector3.up * 1.0f;
            }

            GameObject car = Instantiate(allCarsPrefabs[i], spawnPos, baseSpawnPoint.rotation);
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

            bool isPlayer = (i == selectedIndex);
            var id = car.AddComponent<CarIdentifier>();
            id.isPlayer = isPlayer;

            if (isPlayer)
            {
                var controller = car.AddComponent<CarController>();
                controller.isPlayerControlled = true;
                controller.SetupWheels(car); // TEKERLERİ MANUEL AYARLIYORUZ
                Camera.main.GetComponent<CameraFollow>().SetTarget(car.transform);
                Debug.Log("Oyuncu arabası oluşturuldu ve kontrol atanıyor.");
            }
            else
            {
                var bot = car.AddComponent<SplineBotController>();
                bot.spline = spline;
                bot.offset = i * 0.05f;
                Debug.Log($"Bot {i} spline üzerinde başlatıldı.");
            }
        }
    }
}
