using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class Pulse : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup;

    [SerializeField]
    private float m_PulseTime = 0.2f;

    private float m_ElapsedTime;

    private bool m_IsOn = false;

    private void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0.0f;
        m_ElapsedTime = 0.0f;
    }

	public void Update ()
    {
        m_ElapsedTime += Time.deltaTime;

        if (m_IsOn)
            m_CanvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, m_ElapsedTime / m_PulseTime);
        else
            m_CanvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, m_ElapsedTime / m_PulseTime);

        if (m_ElapsedTime > m_PulseTime)
        {
            m_IsOn = !m_IsOn;
            m_ElapsedTime = 0.0f;
        }
    }
}
