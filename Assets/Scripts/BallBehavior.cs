using UnityEngine;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    public float maxLaunchForce = 10f;
    public Slider powerMeter;
    private Rigidbody rb;
    private bool isCharging = false;
    private float currentLaunchForce = 0f;
    private bool isLaunched = false;
    private bool isPositioning = true;

    // Direction and placement variables
    public Transform ballStartPosition;
    private Vector3 targetPosition;
    public Camera mainCamera;
    public float laneMinX = -1.5f;
    public float laneMaxX = 1.5f;

    // Oscillating Charge
    public float chargeRate = 2f;
    private float chargeTimer = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        powerMeter.value = 0f;
        transform.position = ballStartPosition.position;
        targetPosition = transform.position;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isPositioning && !isLaunched)
        {
            HandlePlacement();
        }

        // Start charging the throw if the spacebar is held down
        if (Input.GetKeyDown(KeyCode.Space) && !isLaunched)
        {
            isCharging = true;
            chargeTimer = 0f;
            currentLaunchForce = 0f;
            powerMeter.value = 0f;
        }

        // Oscillate the force while the spacebar is held down
        if (isCharging && Input.GetKey(KeyCode.Space))
        {
            chargeTimer += Time.deltaTime * chargeRate;
            currentLaunchForce = Mathf.Abs(Mathf.Sin(chargeTimer) * maxLaunchForce);
            powerMeter.value = currentLaunchForce;
        }

            // Launch the ball when the spacebar is released
            if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            LaunchBall();
            isCharging = false;
            powerMeter.value = 0f;
        }
    }

    void HandlePlacement()
    {
        // Raycast from the mouse position to place the ball
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Set the target position within lane boundaries
                float clampedX = Mathf.Clamp(hit.point.x, laneMinX, laneMaxX);
                targetPosition = new Vector3(clampedX, ballStartPosition.position.y, ballStartPosition.position.z);

                transform.position = targetPosition;

                // Aim the ball forward down the lane
                Vector3 direction = Vector3.forward;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation;
            }
        }

        // Lock in placement with right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            isPositioning = false;
        }
    }

    void LaunchBall()
    {
        isLaunched = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * currentLaunchForce, ForceMode.Impulse);
    }
}
