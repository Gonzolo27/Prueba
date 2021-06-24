using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject fadeOut;
    public GameObject deadPanel;
    public GameObject winPanel;

    public float timeToFadeOut;

    /// <summary>
    /// Se para o se arranca el juego en función de como estuviese previamente.
    /// </summary>
    public void TogglePause()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
        if (pausePanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    
    /// <summary>
    /// Se activa el fadeout
    /// </summary>
    private void ActiveFadeOut()
    {
        Time.timeScale = 1;
        FindObjectOfType<PlayerController>().DeactivatePlayer();
        fadeOut.SetActive(true);
    }

    /// <summary>
    /// Se vuelve al menú principal
    /// </summary>
    public void QuitGame()
    {
        ActiveFadeOut();
        GoToNewSceneManager goToNew = FindObjectOfType<GoToNewSceneManager>();
        if (goToNew != null)
        {
            goToNew.LoadScene(fadeOut.GetComponent<Image>(), 0.01f, timeToFadeOut, "MainMenu");
        }
    }

    /// <summary>
    /// Se paraliza al jugador y el tiempo, se quitan sonidos y se colocan los necesarios
    /// Además se activa el panel de victoria
    /// </summary>
    public void Win()
    {
        FindObjectOfType<AudioManager>().audioCanBePlayed = false;
        SFXManager.SharedInstance.StopSteps();
        SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.MUSICWIN);
        Time.timeScale = 0;
        FindObjectOfType<PlayerController>().DeactivatePlayer();
        winPanel.SetActive(true);
    }

    /// <summary>
    /// Se paraliza al jugador y el tiempo, se quitan sonidos y se colocan los necesarios
    /// Además se activa el panel de derrota
    /// </summary>
    public void Dead()
    {
        SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.MUSICDEAD);
        SFXManager.SharedInstance.StopSteps();
        FindObjectOfType<AudioManager>().audioCanBePlayed = false;
        FindObjectOfType<PlayerController>().DeactivatePlayer();
        Time.timeScale = 0;
        deadPanel.SetActive(true);
    }

    /// <summary>
    /// Recarga la escena tras un fadeout.
    /// Está puesto de manera no genérica para agilizar ya que sólo hay una escena jugable.
    /// </summary>
    public void ReloadScene()
    {
        ActiveFadeOut();
        FindObjectOfType<PlayerController>().DeactivatePlayer();
        GoToNewSceneManager goToNew = FindObjectOfType<GoToNewSceneManager>();
        if (goToNew != null)
        {
            goToNew.LoadScene(fadeOut.GetComponent<Image>(), 0.01f, timeToFadeOut, "DemoScene");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }
}
