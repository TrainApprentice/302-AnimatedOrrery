using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetPlanetMat : MonoBehaviour
{

    public Texture[] mats = new Texture[10];
    public MeshRenderer material;

    public string[] planetNames = new string[10];

    public GameObject name;
    // Start is called before the first frame update
    
    public void ApplyMat(int selection)
    {
        if (selection < 0 || selection > mats.Length - 1) return;
        material.material.mainTexture = mats[selection];
        name.GetComponent<TMP_Text>().text = planetNames[selection];
        
    }

}
