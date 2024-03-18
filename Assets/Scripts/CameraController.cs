using UnityEngine;

class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float speed = 360.0f; // degrees per second

    [SerializeField]
    float distance = 5.0f; // distance from player

    [SerializeField]
    float currentDistance = 5.0f;

    float heightOffset = 1.5f;

    // TODO relaxSpeed

    Vector3 GetTargetPosition() { return target.position + heightOffset * Vector3.up; }

    void Update()
    {
        // right drag rotates the camera
        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            float dx = Input.GetAxis("Mouse Y");
            float dy = Input.GetAxis("Mouse X");

            // look up and down by rotating around X-axis
            angles.x = Mathf.Clamp(angles.x + dx * speed * Time.deltaTime, 0.0f, 70.0f);

            // spin the camera round
            angles.y += dy * speed * Time.deltaTime;
            transform.eulerAngles = angles;
        }

        RaycastHit hit;
        if (Physics.Raycast(GetTargetPosition(), -transform.forward, out hit, distance))
        {
            // snap the camera right in to where the collision happened
            currentDistance = hit.distance;
        }
        else
        {
            currentDistance = Mathf.MoveTowards(currentDistance, distance, Time.deltaTime);
        }

        // look at the target point
        transform.position = GetTargetPosition() - currentDistance * transform.forward;
    }
}
