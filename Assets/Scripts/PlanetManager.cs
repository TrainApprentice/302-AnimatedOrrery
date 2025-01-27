using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetManager : MonoBehaviour
{

    public GameObject planetBase, moonBase, nameBase, sun;
    public GameObject cam;

    public Button nextButton, prevButton, sunButton, toggleCameraButton;
    public GameObject timeSliderHandle;
    public GameObject credits;

    private GameObject[] planets = new GameObject[5];
    private List<GameObject> moons = new List<GameObject>();
    private int[] moonsOnPlanets = new int[5];
    private int currPlanet = 5;

    private bool isOrbitCamera = true;
    


    // Start is called before the first frame update
    void Start()
    {
        CreatePlanets(5);
        isOrbitCamera = cam.GetComponent<OrbitCameraRig>();

        if (isOrbitCamera) cam.GetComponent<OrbitCameraRig>().thingToLookAt = (currPlanet != 5) ? planets[currPlanet].transform : sun.transform;
    }
    
    void CreatePlanets(int numPlanets)
    {
        List<int> matNums = new List<int>();
        for (int i = 0; i < numPlanets; i++)
        {
            var newPlanet = Instantiate(planetBase);
            newPlanet.GetComponent<OrbitDemo>().radius = i * 10 + 10;
            newPlanet.GetComponent<OrbitDemo>().orbitCenter = sun.transform;

            int randTexture = (int)Random.Range(0, 10);
            while (matNums.Contains(randTexture)) randTexture = (int)Random.Range(0, numPlanets);

            newPlanet.GetComponent<SetPlanetMat>().ApplyMat(randTexture);
            matNums.Add(randTexture);

            int randomScale = (int)Random.Range(2, 6);
            newPlanet.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            planets[i] = newPlanet;
        }

        AssignMoons();
    }

    void AssignMoons()
    {
        int totalMoons = (int)Random.Range(8, 14);
        int currMoons = totalMoons;
        
        for(int i = 4; i > 0; i--)
        {
            int numMoons = (int)Random.Range(1, currMoons-i);
            while(numMoons > totalMoons/3) numMoons = (int)Random.Range(1, currMoons - i);
            moonsOnPlanets[i] = numMoons;
            for (int j = 0; j < numMoons; j++)
            {
                var newMoon = Instantiate(moonBase);
                newMoon.GetComponent<OrbitDemo>().radius = j + planets[i].transform.localScale.x;
                newMoon.GetComponent<OrbitDemo>().orbitCenter = planets[i].transform;
                moons.Add(newMoon);
            }
            currMoons -= numMoons;
        }
        
        while(currMoons > totalMoons/3)
        {
            int randPlanet = (int)Random.Range(1, 5);
            var newMoon = Instantiate(moonBase);
            newMoon.GetComponent<OrbitDemo>().orbitCenter = planets[randPlanet].transform;
            newMoon.GetComponent<OrbitDemo>().radius = moonsOnPlanets[randPlanet] + planets[randPlanet].transform.localScale.x;
            moons.Add(newMoon);
        }
        

        for(int k = 0; k < currMoons; k++)
        {
            var newMoon = Instantiate(moonBase);
            newMoon.GetComponent<OrbitDemo>().radius = planets[0].transform.localScale.x + k;
            newMoon.GetComponent<OrbitDemo>().orbitCenter = planets[0].transform;
            moons.Add(newMoon);
        }
        moonsOnPlanets[0] = currMoons;
    }

    #region UI Functions
    public void SwapCredits()
    {
        credits.SetActive(!credits.activeInHierarchy);
    }

    public void UpdateTimeMultiplier(float num)
    {
        foreach (GameObject p in planets)
        {
            p.GetComponent<OrbitDemo>().timeMult = num;
        }
        foreach (GameObject m in moons)
        {
            m.GetComponent<OrbitDemo>().timeMult = num;
        }
        timeSliderHandle.GetComponentInChildren<TMP_Text>().text = (Mathf.Round((num * 100)) / 100f).ToString();
        
    }

    public void NextPlanetButton()
    {
        if (currPlanet < 4) currPlanet++;
        else currPlanet = 0;

        if (isOrbitCamera) cam.GetComponent<OrbitCameraRig>().thingToLookAt =  planets[currPlanet].transform;
    }

    public void PrevPlanetButton()
    {
        if (currPlanet > 0) currPlanet--;
        else currPlanet = 4;

        if (isOrbitCamera) cam.GetComponent<OrbitCameraRig>().thingToLookAt = planets[currPlanet].transform;
    }


    public void BackToSunButton()
    {
        currPlanet = 0;
        if (isOrbitCamera) cam.GetComponent<OrbitCameraRig>().thingToLookAt = sun.transform;
    }

    public void ToggleCamera()
    {
       if(isOrbitCamera)
       {
            cam.GetComponent<OrbitCameraRig>().enabled = false;
            cam.GetComponent<FlightCameraRig>().enabled = true;

            nextButton.gameObject.SetActive(false);
            prevButton.gameObject.SetActive(false);
            sunButton.gameObject.SetActive(false);

            toggleCameraButton.GetComponentInChildren<TMP_Text>().text = "Orbital Camera";

            isOrbitCamera = !isOrbitCamera;
       }
       else
       {
            cam.GetComponent<OrbitCameraRig>().enabled = true;
            cam.GetComponent<FlightCameraRig>().enabled = false;

            nextButton.gameObject.SetActive(true);
            prevButton.gameObject.SetActive(true);
            sunButton.gameObject.SetActive(true);

            toggleCameraButton.GetComponentInChildren<TMP_Text>().text = "Free Roam Camera";

            isOrbitCamera = !isOrbitCamera;
        }
    }
    #endregion 


}
