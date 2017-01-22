using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private float m_Range = 0.1f;

    [SerializeField]
    private float m_SmoothTime = 2f;

    private Transform m_Transform;

	private void Start ()
    {
        m_Transform = GetComponent<Transform>();
	}
	
	void Update ()
    {
        Vector3 movement = m_Transform.position;
        movement.y += m_Range * Mathf.Sin(Time.time * m_SmoothTime);

        m_Transform.position = movement;
	}
}
