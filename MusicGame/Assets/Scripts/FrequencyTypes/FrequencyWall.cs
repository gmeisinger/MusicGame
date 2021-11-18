using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyWall : FrequencyBar
{
    protected override void Update()
    {
        float step = speed * Time.deltaTime;

        boxCollider.offset = new Vector2(Mathf.MoveTowards(boxCollider.offset.x, amplitude / 2, step), rect.sizeDelta.y);
        boxCollider.size = new Vector2(Mathf.MoveTowards(boxCollider.size.x, amplitude, step), rect.sizeDelta.y);
        rect.sizeDelta = boxCollider.size;
        growing = amplitude > boxCollider.size.x ? true : false;
        force = growing ? amplitude - boxCollider.size.x : 0f;
    }
}
