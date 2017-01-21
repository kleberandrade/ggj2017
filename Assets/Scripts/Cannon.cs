using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float m_Angle = 90.0f;

    [SerializeField]
    private float m_MaxForce = 10.0f;

    [SerializeField]
    private float m_TimeToMaxForce = 2.0f;

    private float m_Force;

    private Transform m_Transforme;

    private Rigidbody2D m_ParentRigidbody2D;

    private float m_Rotate;

    private bool m_Fire;
    
	private void Start ()
    {
        m_Transforme = GetComponent<Transform>();
        m_ParentRigidbody2D = GetComponentInParent<Rigidbody2D>();
	}
	
	private void Update ()
    {
        Rotate();

        Shooter();
	}

    private void Rotate()
    {
        m_Rotate = Input.GetAxis("Vertical");
        m_Transforme.Rotate(Vector3.forward, m_Rotate * m_Angle * Time.deltaTime);

        Debug.DrawLine(m_Transforme.position, m_Transforme.forward * 100.0f, Color.red);
    }

    private void Shooter()
    {

    }
}
