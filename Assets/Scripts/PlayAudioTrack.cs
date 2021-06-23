using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioTrack : MonoBehaviour
{
    private AudioManager audioManager;
    public int newTrackID;
    public bool playOnStart;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (playOnStart)
        {
            audioManager.PlayNewTrack(newTrackID);
        }
    }
}
