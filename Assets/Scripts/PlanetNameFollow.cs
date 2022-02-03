using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetNameFollow : MonoBehaviour
{
    public GameObject target;

    private GameObject cam;
    private float distance = 0;
    void Start()
    {
        if (target) distance = target.transform.localScale.x;
        cam = GameObject.Find("Orbit Camera");
    }


    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        transform.position = target.transform.position + new Vector3(0, distance, 0);
        FollowCamera();
    }

    void FollowCamera()
    {
        Quaternion lookBase = Quaternion.LookRotation(cam.transform.position);
        Quaternion direction = new Quaternion(lookBase.x * 18, lookBase.y * 18, 0, lookBase.w * 18);
        transform.rotation = direction;
    }
}
