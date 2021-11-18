using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    const float DEFAULT_FREQ_HEIGHT = 5;

    public AudioSource musicSource;
    public List<RectTransform> frequencyParents;
    [Range(4, 96)]
    public int lastFrequency = 24;
    
    IFrequency[,] frequencies;
    float target;

    private void Awake()
    {
        frequencies = new IFrequency[frequencyParents.Count, lastFrequency];

        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();
        for(int i=0;i<frequencyParents.Count;i++)
        {
            for (int j = 0; j < lastFrequency; j++)
            {
                frequencies[i,j] = Instantiate<GameObject>(frequencyParents[i].GetComponent<FrequencyContainer>().frequencyPrefab, frequencyParents[i]).GetComponent<IFrequency>();
                frequencies[i,j].Amplitude = DEFAULT_FREQ_HEIGHT;
            }
        }
    }

    private void Start()
    {

        musicSource.Play();
    }

    private void Update()
    {
        //
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for(int i=0;i<frequencies.GetLength(0);i++)
        {
            for(int j=0;j<frequencies.GetLength(1);j++)
            {
                target = DEFAULT_FREQ_HEIGHT + spectrum[j] * 1000;
                frequencies[i, j].Amplitude = target;
            }
        }
    }
}
