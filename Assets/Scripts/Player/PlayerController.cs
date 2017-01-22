using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_MaxShooterForce = 3.0f;

    private Vector2 m_LeftShooterForce;
    private Vector2 m_RightShooterForce;

    private Rigidbody2D m_Rigidbody2D;

    private Transform m_Transform;

    [SerializeField]
    private Cannon[] m_Cannons;

    [SerializeField]
    private int m_NumberOfExplosion = 4;

    [SerializeField]
    private GameObject m_Explosion;

    private bool m_IsAlive = true;

    public bool IsAlive
    {
        get { return m_IsAlive; }
    }

    private void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_IsAlive = true;
    }

    /// <summary>
    ///  Método para matar o jogador
    /// </summary>
    public void Die()
    {
        StartCoroutine("Exploding");
    }

    private IEnumerator Exploding()
    {
        GameObject[] explosions = new GameObject[m_NumberOfExplosion];
        for (int i = 0; i < m_NumberOfExplosion; i++)
        {
            float x = m_Transform.position.x + Random.Range(-64.0f, 64.0f);
            float y = m_Transform.position.x + Random.Range(-64.0f, 64.0f);
            explosions[i] = Instantiate(m_Explosion, new Vector3(x, y, 0.0f), m_Transform.rotation);
            Destroy(explosions[i], 3.0f);
            yield return new WaitForSeconds(0.25f);
        }

        m_IsAlive = false;
        Pause();

        m_Explosion.gameObject.SetActive(true);

        yield return null;
    }

    public void Resume()
    {
        for (int i = 0; i < m_Cannons.Length; i++)
            m_Cannons[i].enabled = true;
    }

    public void Pause()
    {
        for (int i = 0; i < m_Cannons.Length; i++)
            m_Cannons[i].enabled = false;
    }
}
