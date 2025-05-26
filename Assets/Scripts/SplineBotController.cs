using UnityEngine;

public class SplineBotController : MonoBehaviour
{
    public Spline spline;
    public float baseSpeed = 8f; // Ortalama hız
    public float t = 0f;         // 0-1 arası ilerleme
    public float offset = 0f;    // Sağ/sol kayma

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseSpeed += Random.Range(-0.5f, 0.5f); // Çok az hız farkı
        t = Mathf.Clamp01(offset); // Her bot farklı noktada başlasın

        if (spline == null)
            Debug.LogError($"{gameObject.name} için spline atanmadı!");
    }

    void FixedUpdate()
    {
        if (!RaceCountdownManager.Instance.raceStarted || spline == null) return;

        // Δt artışı sabit hızla olacak şekilde ayarlandı
        float splineDistance = spline.TotalLength(); // tüm spline uzunluğu
        float delta = (baseSpeed / splineDistance) * Time.fixedDeltaTime;
        t += delta;
        if (t > 1f) t -= 1f;

        Vector3 currentPos = spline.GetPoint(t);
        Vector3 lookAhead = spline.GetPoint(Mathf.Min(t + 0.01f, 1f));
        Vector3 forward = (lookAhead - currentPos).normalized;

        Vector3 right = Vector3.Cross(Vector3.up, forward);
        Vector3 offsetPos = currentPos + right.normalized * offset;

        offsetPos.y = transform.position.y; // zemine sabit kal

        rb.MovePosition(Vector3.Lerp(transform.position, offsetPos, 1f));
        if (forward != Vector3.zero)
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(forward), 5f * Time.fixedDeltaTime));
    }
}