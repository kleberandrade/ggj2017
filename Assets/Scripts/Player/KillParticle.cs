using UnityEngine;

public class KillParticle : MonoBehaviour
{
    private ParticleSystem m_ParticleSystem;

    private void Start ()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (m_ParticleSystem.isPlaying)
            return;

        Destroy(gameObject);
    }
}