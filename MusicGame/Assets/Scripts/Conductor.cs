using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    public float songBpm;
    public float firstBeatOffset;
    public AudioSource musicSource;

    float secPerBeat;
    float songPosition;
    float songPositionInBeats;
    float dspSongTime;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();

        secPerBeat = 60f / songBpm;

        //record the time when themusic starts
        dspSongTime = (float)AudioSettings.dspTime;

        //start music
        musicSource.Play();
    }

    private void Update()
    {
        //seconds since song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //beats since song started
        songPositionInBeats = songPosition / secPerBeat;
    }
}
