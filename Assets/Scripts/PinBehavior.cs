using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;
    private bool isKnockedDown = false;

    public float fallThreshold = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
}


