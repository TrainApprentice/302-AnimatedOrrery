using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitDemo : MonoBehaviour
{

    public Transform orbitCenter;
    public bool doFlip = false;

    public float radius = 2;
    public float speed = 1;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(0, 3);
        speed = Random.Range(.2f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!orbitCenter) return;

        timer += Time.deltaTime * speed;

        var x = Mathf.Cos(timer) * radius;
        var y = orbitCenter.position.y;
        var z = Mathf.Sin(timer) * radius;

        transform.position = new Vector3(x, y, z) + orbitCenter.position;
    }
}
