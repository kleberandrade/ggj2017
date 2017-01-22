using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float m_DampTime = 0.2f;

    [SerializeField]
    private float m_ScreenEdgeBuffer = 4f;

    [SerializeField]
    private float m_MinSize = 3f;         

    private Transform[] m_Targets;

    private Camera m_Camera;          

    private float m_ZoomSpeed;        

    private Vector3 m_MoveVelocity;   

    private Vector3 m_DesiredPosition;

    private Transform m_Transform;

	private bool m_Loaded = false;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
		if (!m_Loaded) {
			m_Camera = GetComponent<Camera> ();
			m_Transform = GetComponent<Transform> ();

			GameObject[] gos = GameObject.FindGameObjectsWithTag ("Player");
			m_Targets = new Transform[gos.Length];
			for (int i = 0; i < gos.Length; i++)
				m_Targets [i] = gos [i].GetComponent<Transform> ();
			m_Loaded = gos.Length > 0;
		}
        Move();

        Zoom();
    }


    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            averagePos += m_Targets[i].position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.z = transform.position.z;

        m_DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        float size = 0f;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        size += m_ScreenEdgeBuffer;

        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        FindAveragePosition();

        transform.position = m_DesiredPosition;

        m_Camera.orthographicSize = FindRequiredSize();
    }
}