using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCameraRig : MonoBehaviour
{
    public Transform thingToLookAt;
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    public float scrollSensitivity = 2f;

    private Camera cam;

    private float pitch = 0, yaw = 0;
    private float zoom = 10;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        /*
        Vector3 directionToTarget = thingToLookAt.position - cam.transform.position;

        Vector3 targetPos = -directionToTarget;
        targetPos.Normalize();
        targetPos *= zoom;

        targetPos += thingToLookAt.position;

        cam.transform.position = AnimMath.Ease(cam.transform.position, targetPos, .001f);

        cam.transform.rotation = Quaternion.LookRotation(directionToTarget);
        */
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivityX; // Yaw (y)
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivityY; // Pitch (x)

            pitch = Mathf.Clamp(pitch, -89, 89);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        } 
        
        

        // Position
        if (thingToLookAt == null) return;
        if (Vector3.Distance(transform.position, thingToLookAt.position) > 1f) transform.position = AnimMath.Ease(transform.position, thingToLookAt.position, .001f);
        else transform.position = thingToLookAt.position;
        

        // Dolly
        Vector2 scrollAmt = Input.mouseScrollDelta;
        zoom -= scrollAmt.y * scrollSensitivity;

        zoom = Mathf.Clamp(zoom, 10, 70);

        float z = AnimMath.Ease(cam.transform.localPosition.z, -zoom, .01f);
        
        cam.transform.localPosition = new Vector3(0, 0, z);

    }
}
