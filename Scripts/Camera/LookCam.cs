using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class LookCam : MonoBehaviour
{

    public Transform target;
    public float distance = 8.0f;
    public float xSpeed = 60.0f;
    public float ySpeed = 60.0f;

    public float yMinLimit = 10f;
    public float yMaxLimit = 80f;

    public float distanceMin = 0.5f;
    public float distanceMax = 15f;

    public int layerMask;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

        layerMask = LayerMask.GetMask("Wolf", "Main Camera");
    }

    void LateUpdate()
    {
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        if (target)
        {
            if(Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Space))
            {
                Cursor.lockState = CursorLockMode.Locked;

                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }
            if(Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Space))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        Quaternion rot = Quaternion.Euler(y, x, 0);

        RaycastHit hit;
        if (Physics.Linecast(target.position, transform.position, out hit, layerMask))
        {
            distance -= hit.distance;
        }
        Vector3 negDist = new Vector3(0.0f, 0.0f, -distance);
        Vector3 pos = rot * negDist + target.position;

        transform.rotation = rot;
        transform.position = pos;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}