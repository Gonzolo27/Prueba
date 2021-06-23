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

    

    private void ActiveFadeOut()
    {
        Time.timeScale = 1;
        FindObjectOfType<PlayerController>().DeactivatePlayer();
        fadeOut.SetActive(true);
    }
    public void QuitGame()
    {
        ActiveFadeOut();
        GoToNewSceneManager goToNew = FindObjectOfType<GoToNewSceneManager>();
        if (goToNew != null)
        {
            goToNew.LoadScene(fadeOut.GetComponent<Image>(), 0.01f, timeToFadeOut, "MainMenu");
        }
    }

    public void Win()
    {
        SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.MUSICWIN);
        Time.timeScale = 0;
        FindObjectOfType<PlayerController>().DeactivatePlayer();
        winPanel.SetActive(true);
    }

    public void Dead()
    {
        SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.MUSICDEAD);
        FindObjectOfType<AudioManager>().audioCanBePlayed = false;
        FindObjectOfType<PlayerController>().DeactivatePlayer();
        Time.timeScale = 0;
        deadPanel.SetActive(true);
    }

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

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }
}
