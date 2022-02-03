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
        transform.LookAt(cam.transform);
        //FollowCamera();
    }

}
