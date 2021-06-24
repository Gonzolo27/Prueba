using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource hit, beeDead, chickenDead, musicWin, musicDead, playerHitted, jump, steps, takeItem, _throw;
    public enum SFXType { HIT, BEEDEAD, CHICKENDEAD, MUSICWIN, MUSICDEAD, PLAYERHITTED, JUMP, STEPS, TAKEITEM, THROW};

    private static SFXManager sharedInstance = null;

    public static SFXManager SharedInstance
    {
        get{ return sharedInstance; }
    }


    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    //Los siguientes 2 métodos no los hago genéricos para agilizar la prueba.
    /// <summary>
    /// Devuelve true o false en funicón de si se están reproduciendo o no los pasos.
    /// </summary>
    /// <returns></returns>
    public bool IsPlayingSteps()
    {
        return steps.isPlaying;
    }

    /// <summary>
    /// Se para el sonido de los pasos
    /// </summary>
    public void StopSteps()
    {
        if (steps.isPlaying)
        {
            steps.Stop();
        }
            
    }

    /// <summary>
    /// Según el type seleccionado se reproduce un sonido u otro
    /// </summary>
    /// <param name="type">Elemento del enumerado</param>
    public void PlaySFX(SFXType type)
    {
        switch (type)
        {
            case SFXType.BEEDEAD:
                beeDead.Play();
                break;
            case SFXType.CHICKENDEAD:
                chickenDead.Play();
                break;
            case SFXType.HIT:
                hit.Play();
                break;
            case SFXType.MUSICWIN:
                musicWin.Play();
                break;
            case SFXType.MUSICDEAD:
                musicDead.Play();
                break;
            case SFXType.PLAYERHITTED:
                playerHitted.Play();
                break;
            case SFXType.JUMP:
                jump.Play();
                break;
            case SFXType.STEPS:
                steps.Play();
                break;
            case SFXType.TAKEITEM:
                takeItem.Play();
                break;
            case SFXType.THROW:
                _throw.Play();
                break;
        }
    }

}
