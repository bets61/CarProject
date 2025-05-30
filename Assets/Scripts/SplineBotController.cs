using UnityEngine;

public class SplineBotController : MonoBehaviour
{
    public Spline spline;
    public float baseSpeed = 8f;
    public float t = 0f;
    public float offset = 0f;

    private Rigidbody rb;
    private bool started = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (spline == null)
            Debug.LogError("Spline atanmadı!");
    }

    void FixedUpdate()
    {
        if (spline == null || rb == null) return;

        if (!RaceCountdownManager.Instance.raceStarted)
        {
            if (!started)
            {
                Vector3 currentPos = spline.GetPoint(t);
                Vector3 lookAhead = spline.GetPoint(Mathf.Min(t + 0.01f, 1f));
                Vector3 forward = (lookAhead - currentPos).normalized;
                Vector3 right = Vector3.Cross(Vector3.up, forward);
                Vector3 offsetPos = currentPos + right * offset;
                offsetPos.y = transform.position.y;

                rb.MovePosition(offsetPos);
                rb.rotation = Quaternion.LookRotation(forward);
            }
            return;
        }

        if (!started)
        {
            started = true;
            Debug.Log($"{gameObject.name} yarışa spline üzerinden başladı! 🚗 Hızı: {baseSpeed}");
        }

        float splineDistance = spline.TotalLength();
        float delta = (baseSpeed / splineDistance) * Time.fixedDeltaTime;
        t += delta;
        if (t > 1f) t -= 1f;

        Vector3 pos = spline.GetPoint(t);
        Vector3 look = spline.GetPoint(Mathf.Min(t + 0.01f, 1f));
        Vector3 dir = (look - pos).normalized;
        Vector3 rightVec = Vector3.Cross(Vector3.up, dir);
        Vector3 finalPos = pos + rightVec * offset;
        finalPos.y = transform.position.y;

        rb.MovePosition(Vector3.Lerp(transform.position, finalPos, 1f));
        if (dir != Vector3.zero)
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(dir), 5f * Time.fixedDeltaTime));
    }
}
