using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A typical frequency bar, can grow vertically or horizontally.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class FrequencyBar : MonoBehaviour, IFrequency
{
    public bool horizontal = false;

    protected float speed = 300f;

    protected float force;
    protected bool growing = false;

    protected RectTransform rect;
    protected BoxCollider2D boxCollider;

    [SerializeField]
    protected float amplitude;
    public float Amplitude 
    {
        get => amplitude;
        set => amplitude = value;
    }

    void Awake()
    {
        rect = transform as RectTransform;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void SetColliderSize()
    {
        boxCollider.offset = new Vector2(rect.sizeDelta.x / 2, rect.sizeDelta.y / 2);
        boxCollider.size = rect.sizeDelta;
    }

    virtual protected void Update()
    {
        if(horizontal)
        {
            UpdateHorizontal();
        }
        else
        {
            UpdateVertical();
        }
    }

    private void UpdateVertical()
    {
        if (boxCollider.offset.x != rect.sizeDelta.x)
        {
            SetColliderSize();
        }

        float step = speed * Time.deltaTime;

        boxCollider.offset = new Vector2(boxCollider.offset.x, Mathf.MoveTowards(boxCollider.offset.y, amplitude / 2, step));
        boxCollider.size = new Vector2(rect.sizeDelta.x, Mathf.MoveTowards(boxCollider.size.y, amplitude, step));
        rect.sizeDelta = boxCollider.size;
        growing = amplitude > boxCollider.size.y;
        force = growing ? amplitude - boxCollider.size.y : 0f;
    }

    private void UpdateHorizontal()
    {
        if (boxCollider.offset.y != rect.sizeDelta.y)
        {
            SetColliderSize();
        }

        float step = speed * Time.deltaTime;

        boxCollider.offset = new Vector2(Mathf.MoveTowards(boxCollider.offset.x, amplitude / 2, step), boxCollider.offset.y);
        boxCollider.size = new Vector2(Mathf.MoveTowards(boxCollider.size.x, amplitude, step), rect.sizeDelta.y);
        rect.sizeDelta = boxCollider.size;
        growing = amplitude > boxCollider.size.x ? true : false;
        force = growing ? amplitude - boxCollider.size.x : 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (growing)
        {
            collision.collider.attachedRigidbody.AddForceAtPosition(((Vector2)collision.gameObject.transform.position - collision.contacts[0].point).normalized * force, collision.contacts[0].point);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(growing)
        {
            collision.collider.attachedRigidbody.AddForceAtPosition(((Vector2)collision.gameObject.transform.position - collision.contacts[0].point).normalized * force, collision.contacts[0].point);
        }
    }
}
