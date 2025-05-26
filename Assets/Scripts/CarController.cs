using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool isPlayerControlled = false;

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;
    private bool isBraking;

    [SerializeField] public float motorForce = 2000f;
    [SerializeField] public float brakeForce = 3000f;
    [SerializeField] public float maxSteerAngle = 20f;

    private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    private Transform frontLeftWheelTransform, frontRightWheelTransform;
    private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.7f, 0);
    }

    private void FixedUpdate()
    {
        if (!RaceCountdownManager.Instance.raceStarted)
        {
            ApplyBrakes(5000f);
            return;
        }

        if (!isPlayerControlled) return;

        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        if (rearLeftWheelCollider == null || rearRightWheelCollider == null)
        {
            Debug.LogError("Motor torque uygulanamıyor! Arka tekerlek collider'ları eksik.");
            return;
        }

        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBrakes(currentBrakeForce);

        if (!rearLeftWheelCollider.isGrounded || !rearRightWheelCollider.isGrounded)
        {
            Debug.Log("Arka tekerlekler yere temas etmiyor!");
            return;
        }

      /*  if (rearLeftWheelCollider != null)
        {
            Debug.Log("Grounded: " + rearLeftWheelCollider.isGrounded);
        }*/

    }

    private void ApplyBrakes(float force)
    {
        frontLeftWheelCollider.brakeTorque = force;
        frontRightWheelCollider.brakeTorque = force;
        rearLeftWheelCollider.brakeTorque = force;
        rearRightWheelCollider.brakeTorque = force;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider collider, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }

    public void SetupWheels(GameObject car)
    {
        frontLeftWheelCollider = car.transform.Find("Wheels/Colliders/FrontLeftWheel").GetComponent<WheelCollider>();
        frontRightWheelCollider = car.transform.Find("Wheels/Colliders/FrontRightWheel").GetComponent<WheelCollider>();
        rearLeftWheelCollider = car.transform.Find("Wheels/Colliders/RearLeftWheel").GetComponent<WheelCollider>();
        rearRightWheelCollider = car.transform.Find("Wheels/Colliders/RearRightWheel").GetComponent<WheelCollider>();

        frontLeftWheelTransform = car.transform.Find("Wheels/Meshes/FrontLeftWheel");
        frontRightWheelTransform = car.transform.Find("Wheels/Meshes/FrontRightWheel");
        rearLeftWheelTransform = car.transform.Find("Wheels/Meshes/RearLeftWheel");
        rearRightWheelTransform = car.transform.Find("Wheels/Meshes/RearRightWheel");

        Debug.Log("Tekerlek setup tamamlandı.");
    }
}
