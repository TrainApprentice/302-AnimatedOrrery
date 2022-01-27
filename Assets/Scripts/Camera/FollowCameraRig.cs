using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraRig : MonoBehaviour
{
    public Transform target;

    public float desiredDistance = 20;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget = target.position - transform.position;

        Vector3 targetPos = -directionToTarget;
        targetPos.Normalize();
        targetPos *= desiredDistance;

        targetPos += target.position;

        transform.position = AnimMath.Ease(transform.position, targetPos, .001f);

        transform.rotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        
    }

    
}
