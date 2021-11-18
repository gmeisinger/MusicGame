using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a frequency range, needs to be able to set the amplitude
/// </summary>
public interface IFrequency
{
    public float Amplitude { get; set; }
}
