using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{

    public GameObject planetBase, moonBase, sun;
    public GameObject cam;

    private GameObject[] planets = new GameObject[5];
    private int currPlanet = 5;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlanets();
        cam.GetComponent<CameraFollow>().target = planets[currPlanet].transform;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (currPlanet < 5) currPlanet++;
            else currPlanet = 0;

            cam.GetComponent<CameraFollow>().target = (currPlanet != 5) ? planets[currPlanet].transform : sun.transform;
        }
    }
    

    void CreatePlanets()
    {
        for (int i = 0; i < 5; i++)
        {
            var newPlanet = Instantiate(planetBase);
            newPlanet.GetComponent<OrbitDemo>().radius = i * 8 + 10;
            newPlanet.GetComponent<OrbitDemo>().orbitCenter = sun.transform;

            int randomScale = (int)Random.Range(2, 6);
            newPlanet.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            planets[i] = newPlanet;
        }

        AssignMoons();
    }

    void AssignMoons()
    {
        int totalMoons = (int)Random.Range(8, 16);
        
        for(int i = 4; i > 0; i--)
        {
            int numMoons = (int)Random.Range(1, totalMoons - i);
            for(int j = 0; j < numMoons; j++)
            {
                var newMoon = Instantiate(moonBase);
                newMoon.GetComponent<OrbitDemo>().radius = j * 2;
                newMoon.GetComponent<OrbitDemo>().orbitCenter = planets[i].transform;
            }
            totalMoons -= numMoons;
        }

        for(int k = 0; k < totalMoons; k++)
        {
            var newMoon = Instantiate(moonBase);
            newMoon.GetComponent<OrbitDemo>().radius = 2;
            newMoon.GetComponent<OrbitDemo>().orbitCenter = planets[0].transform;
        }
    }

    
}
