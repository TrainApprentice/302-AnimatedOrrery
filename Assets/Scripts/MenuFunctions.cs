using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void ToSpace()
    {
        SceneManager.LoadScene("Planets");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}