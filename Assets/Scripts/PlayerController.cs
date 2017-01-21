using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_MaxShooterForce = 3.0f;

    private Vector2 m_LeftShooterForce;
    private Vector2 m_RightShooterForce;

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	private void Update ()
    {


        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //rb2d.AddForce(movement * speed);
    }
}
