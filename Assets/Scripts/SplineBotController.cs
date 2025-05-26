using UnityEngine;

public class SplineBotController : MonoBehaviour
{
    public Spline spline;
    public float moveSpeed = 10f;
    public float t = 0f;         // 0-1 arası konum
    public float offset = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed += Random.Range(-2f, 2f);
        t = offset;
    }

    void FixedUpdate()
    {
        if (!RaceCountdownManager.Instance.raceStarted || spline == null)
            return;

        t += Time.fixedDeltaTime * (moveSpeed / 100f);
        if (t > 1f) t -= 1f;

        Vector3 nextPos = spline.GetPoint(t);
        Vector3 lookAhead = spline.GetPoint(Mathf.Clamp01(t + 0.02f));  // Daha uzağa bak

        Vector3 direction = (lookAhead - transform.position).normalized;

        // Konumu ve yönü spline'a göre ayarla
        rb.MovePosition(Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.fixedDeltaTime));
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, 5f * Time.fixedDeltaTime));
        }
    }
}
