using UnityEngine;

public class Vision : MonoBehaviour
{
    private Transform m_Transform;

    [SerializeField]
    private float m_Range = 0.7f;

    private Vector3 m_OriginalScale;

    [SerializeField]
    private float m_RepeatTime = 0.5f;

    private void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_OriginalScale = m_Transform.localScale;
        InvokeRepeating("ChangeVision", 0.0f, m_RepeatTime);
    }

    private void ChangeVision()
    {
        m_Transform.localScale = m_OriginalScale + Random.insideUnitSphere * m_Range;
    }
}
