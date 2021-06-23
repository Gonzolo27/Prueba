using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GoToNewSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GoToNewSceneManager _instance;
    IEnumerator coroutine;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadScene(Image m_renderer, float steps, float timeToFadeout, string sceneName)
    {
        timeToFadeout *= steps;
        coroutine = FadeOut(m_renderer, steps, timeToFadeout, sceneName);
        StartCoroutine(coroutine);
    }
    IEnumerator FadeOut(Image m_renderer, float steps, float timeToFadeout, string sceneName)
    {
        for (float f = 0f; f <= 1.05f; f += steps)
        {
            Color c = m_renderer.color;
            c.a = f;
            m_renderer.color = c;
            yield return new WaitForSeconds(timeToFadeout);
        }
        SceneManager.LoadScene(sceneName);
    }
}
