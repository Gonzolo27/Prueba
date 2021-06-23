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

    public void PlayNewTrack(int newTrack)
    {
        StopActual();
        currentTrack = newTrack;
        audioTracks[currentTrack].Play();
    }

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
