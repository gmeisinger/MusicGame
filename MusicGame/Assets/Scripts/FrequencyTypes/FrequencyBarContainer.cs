using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for frequency bar. Used with horizontal or vertical layout groups.
/// </summary>
public class FrequencyBarContainer : MonoBehaviour
{
    public GameObject frequencyPrefab;
    public bool growHorizontal;

    public IFrequency InstantiateFrequency()
    {
        GameObject frequencyObj = Instantiate<GameObject>(frequencyPrefab, transform);
        frequencyObj.GetComponent<FrequencyBar>().horizontal = growHorizontal;
        return frequencyObj.GetComponent<IFrequency>();
    }
}
