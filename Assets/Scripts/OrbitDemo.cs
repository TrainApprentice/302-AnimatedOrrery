using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class OrbitDemo : MonoBehaviour
{

    public Transform orbitCenter;
    private LineRenderer linePath;

    public float radius = 2;
    public float speed = 1;
    public bool isMoon = false;

    private float rotSpeed = 1;
    private float currRot = 0;
    private float timer = 0;
    public float timeMult = 1f;
    // Start is called before the first frame update
    void Start()
    {
        linePath = GetComponent<LineRenderer>();
        timer = Random.Range(0, 6);
        speed = Random.Range(.2f, .8f);
        rotSpeed = (isMoon) ? Random.Range(.02f, .05f) : Random.Range(.03f, .08f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!orbitCenter) return;

        timer += Time.deltaTime * timeMult;
        currRot += rotSpeed * timeMult;


        var x = Mathf.Cos(timer * speed) * radius;
        var y = orbitCenter.position.y;
        var z = Mathf.Sin(timer * speed) * radius;

        transform.position = new Vector3(x, y, z) + orbitCenter.position;

        transform.rotation = Quaternion.Euler(0, currRot, 0);

        if (orbitCenter.hasChanged && !isMoon) UpdateOrbitPath();
    }

    void UpdateOrbitPath()
    {
        if (!orbitCenter) return;

        int res = 36;

        Vector3[] points = new Vector3[res];

        float conv = Mathf.PI / (res * .5f);

        for(int i = 0; i < points.Length; i++)
        {
            float x = Mathf.Cos(i * conv) * radius;
            float z = Mathf.Sin(i * conv) * radius;

            points[i] = new Vector3(x, 0, z) + orbitCenter.position;
        }
        linePath.positionCount = res;
        linePath.SetPositions(points);
    }
}
