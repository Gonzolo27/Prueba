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

    /// <summary>
    /// Se calcula el tiempo de espera según el número de iteraciones que se vaya a realizar
    /// Se recarga la escena con una corrutina
    /// </summary>
    /// <param name="m_renderer">Imagen a la que se le va a cambiar el alpha</param>
    /// <param name="steps">Número de iteraciones del bucle de la corrutina</param>
    /// <param name="timeToFadeout">Tiempo real que se desea que dure la corrutina. Ejemplo: 2sg</param>
    /// <param name="sceneName">Nombre de la escena a cargar</param>
    public void LoadScene(Image m_renderer, float steps, float timeToFadeout, string sceneName)
    {
        timeToFadeout *= steps;
        coroutine = FadeOut(m_renderer, steps, timeToFadeout, sceneName);
        StartCoroutine(coroutine);
    }

    /// <summary>
    /// Se hace un fade out durante un periodo de tiempo y se carga la escena
    /// </summary>
    /// <param name="m_renderer">Imagen a la que se le va a cambiar el alpha</param>
    /// <param name="steps">Número de iteraciones del bucle</param>
    /// <param name="timeToFadeout">Tiempo (modificado ya) que hay que esperar para que se realice otra iteración</param>
    /// <param name="sceneName">Nombre de la escena a cargar</param>
    /// <returns></returns>
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
