using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioTracks;
    public int currentTrack;
    public bool audioCanBePlayed;

    private AudioManager _instance;
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
    /// Se para la actual pista de audio y se pone a play la nueva.
    /// </summary>
    /// <param name="newTrack">Índice del array que contiene las pistas de audio</param>
    public void PlayNewTrack(int newTrack)
    {
        StopActual();
        currentTrack = newTrack;
        audioTracks[currentTrack].Play();
    }

    /// <summary>
    /// Se para el audio sonando actualmente
    /// Para que no fuese tan brusco, a la vez que se hace el fadeout de la escena
    /// Se podría ir haciendo un fadeout del sonido.
    /// </summary>
    private void StopActual()
    {
        audioTracks[currentTrack].Stop();
    }

    void Update()
    {
        if (audioCanBePlayed)
        {
            if (!audioTracks[currentTrack].isPlaying)
            {
                audioTracks[currentTrack].Play();
            }
        }
        else 
        {
            StopActual();
        }
    }
}
