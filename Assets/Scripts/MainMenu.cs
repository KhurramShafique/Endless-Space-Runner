using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // You need to import this to use SceneManager

public class MainMenu : MonoBehaviour
{
    public string playGameLevel; // The name of the scene to load

    public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel); // Corrected SceneManager spelling
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
