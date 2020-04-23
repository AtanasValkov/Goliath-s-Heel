using UnityEngine;

public class RotateGrappler : MonoBehaviour
{
    private Grappler grappler;
    public float rotationSpeed = 5f;

    private Quaternion desiredRotation;

    void Awake()
    {
        grappler = GetComponent<Grappler>();
    }

    void Update()
    {
        if (!grappler.IsGrappling())
        {
            desiredRotation = transform.parent.rotation;
        }
        else
        {
            desiredRotation = Quaternion.LookRotation(grappler.GetGrapplePoint() - transform.position);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
