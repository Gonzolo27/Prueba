using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject fadeOut;
    public GameObject deadPanel;

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

    public void QuitGame()
    {
        Time.timeScale = 1;
        DeactivatePlayer();
        fadeOut.SetActive(true);
        FindObjectOfType<GoToNewSceneManager>().LoadScene(fadeOut.GetComponent<Image>(), 0.01f, timeToFadeOut, "MainMenu");
    }

    public void Dead()
    {
        DeactivatePlayer();
        Time.timeScale = 0;
        deadPanel.SetActive(true);
    }

    private void DeactivatePlayer()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        pc.enabled = false;
        pc.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        pc.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        pc.gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
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
