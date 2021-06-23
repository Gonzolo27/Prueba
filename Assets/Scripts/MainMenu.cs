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


    public void StartGame()
    {
        //IEnumerator coroutine;
        //timeToFadeOut *= steps;
        fadeOut.SetActive(true);
        GoToNewSceneManager goToNew = FindObjectOfType<GoToNewSceneManager>();
        if (goToNew != null)
        {
            goToNew.LoadScene(fadeOut.GetComponent<Image>(), 0.01f, timeToFadeOut, "DemoScene");
        }
        //coroutine = FadeOut(m_renderer, steps);
        //StartCoroutine(coroutine);
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando game");
        Application.Quit();
    }

   /* IEnumerator FadeOut(Image m_renderer, float steps)
    {
        for (float f = 0f; f <= 1.05f; f += steps)
        {
            Color c = m_renderer.color;
            c.a = f;
            m_renderer.color = c;
            yield return new WaitForSeconds(timeToFadeOut);
        }
        //SceneManager.LoadScene("DemoScene");
    }*/
}
