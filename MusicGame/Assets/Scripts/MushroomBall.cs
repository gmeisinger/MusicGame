using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBall : MonoBehaviour
{
    ParticleSystem m_ParticleSystem;
    SpriteRenderer m_SpriteRenderer;
    Collider2D m_Collider;

    [SerializeField]
    float hitForce = 100f;

    private void Awake()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<Collider2D>();
    }

    void Explode()
    {
        m_Collider.enabled = false;
        m_SpriteRenderer.enabled = false;
        m_ParticleSystem.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.attachedRigidbody.AddForceAtPosition(((Vector2)collision.gameObject.transform.position - collision.contacts[0].point).normalized * hitForce, collision.contacts[0].point);
    }
}
