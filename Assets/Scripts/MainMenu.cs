using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public GameObject fadeOut;
    public float timeToFadeOut;
    private GoToNewSceneManager goToNewS;

    /// <summary>
    /// se inicia el juego al pulsar Empezar juego en el menú principal
    /// </summary>
    public void StartGame()
    {
        fadeOut.SetActive(true);
        GoToNewSceneManager goToNew = FindObjectOfType<GoToNewSceneManager>();
        if (goToNew != null)
        {
            goToNew.LoadScene(fadeOut.GetComponent<Image>(), 0.01f, timeToFadeOut, "DemoScene");
        }
    }

    /// <summary>
    /// Se cierra el juego al pulsar salir en el menú principal
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Cerrando game");
        Application.Quit();
    }
}
