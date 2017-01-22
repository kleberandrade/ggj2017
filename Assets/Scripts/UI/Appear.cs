using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Appear : MonoBehaviour
{
    [SerializeField]
    private float m_Time = 2.0f;

    private CanvasGroup m_CanvasGroup;

    private Transform m_Transform;

    private float m_ElapsedTime = 0.0f;

    [SerializeField]
    private bool m_RandomStart = false;

    private void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0.0f;

        if (m_RandomStart)
            m_Time = Random.Range(0.0f, 3.0f);
    }
    
	// Update is called once per frame
	private void Update ()
    {
        m_CanvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, m_ElapsedTime / m_Time);
        m_ElapsedTime += Time.deltaTime;
	}
}
