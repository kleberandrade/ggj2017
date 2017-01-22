using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Cannon : MonoBehaviour
{
    [SerializeField]
    private int m_PlayerNumber;

    [SerializeField]
    private string m_CanvasTag = "HUD";

    [Header("Sensitivity")]

    [SerializeField]
    private float m_Angle = 90.0f;

    [Header("Force")]

    [SerializeField]
    private float m_MaxForce = 1000.0f;

    [SerializeField]
    private float m_ForceTime = 1.0f;

    [SerializeField]
    private Slider m_ForceSlider;

    [Header("Cooldown")]

    [SerializeField]
    private float m_CooldownTime = 1.0f;

    [SerializeField]
    private Slider m_CooldownSlider;

    [Header("Shoot")]
    [SerializeField]
    private bool m_UserOneParticle;

    [SerializeField]
    private int m_MaxParticles = 10;

    [SerializeField]
    private float m_IntervalToNewParticle = 0.2f;

    [SerializeField]
    private ParticleSystem m_CannonParticle;

    [SerializeField]
    private ParticleSystem m_CannonOneParticle;

    [SerializeField]
    private bool m_AttachParticle = false;

    private Transform m_CannonPoint;

    private AudioSource m_CannonAudio;

    private float m_Cooldown;

    private float m_CooldownElapsedTime = 0.0f;

    private bool m_ReadyToShot = false;

    private float m_Force;

    private Transform m_Transforme;

    private Rigidbody2D m_ParentRigidbody2D;

    private float m_Rotate = 0.0f;

    private float m_PressedElapsedTime = 0.0f;
    
	private void Start ()
    {
        m_Transforme = GetComponent<Transform>();
        m_ParentRigidbody2D = GetComponentInParent<Rigidbody2D>();
        m_CannonAudio = GetComponent<AudioSource>();
        m_CannonPoint = GetComponentInChildren<Transform>();

        Transform canvas = GameObject.FindGameObjectWithTag(m_CanvasTag).transform;
        m_CooldownSlider = canvas.FindChild("TimerAndPower/P" + m_PlayerNumber + "Cooldown").GetComponent<Slider>();
        m_ForceSlider = canvas.FindChild("TimerAndPower/P" + m_PlayerNumber + "PowerBar").GetComponent<Slider>();
    }
	
	private void Update ()
    {
        Rotate();

        if (m_ReadyToShot)
        {
            ShootLoader();

            Shoot();
        }
        else
        {
            CooldownLoader();
        }

        UpdateUI();
	}

    private void Rotate()
    {
        m_Rotate = Input.GetAxis("Vertical" + m_PlayerNumber);
        m_Transforme.Rotate(Vector3.forward, m_Rotate * m_Angle * Time.deltaTime);

#if UNITY_EDITOR
        Vector3 direction = Helper.TransformToDirection2D(m_Transforme);
        Debug.DrawRay(m_Transforme.position, direction, Color.red, 0.2f);
#endif
    }

    private void ShootLoader()
    {
        if (Input.GetButton("Fire" + m_PlayerNumber))
        {
            m_Force = Mathf.Lerp(0.0f, m_MaxForce, m_PressedElapsedTime);
            m_Force = Mathf.Clamp(m_Force, 0.0f, m_MaxForce);
            m_PressedElapsedTime += Time.deltaTime * (1.0f / m_ForceTime);
        }
    }

    private void Shoot()
    {
        if (Input.GetButtonUp("Fire" + m_PlayerNumber))
        {
            Vector3 direction = Helper.TransformToDirection2D(m_Transforme);
            m_ParentRigidbody2D.AddForceAtPosition(-1.0f * direction * m_Force, m_Transforme.position, ForceMode2D.Impulse);

            ShootParticle();

            ResetForce();
            ResetCooldown();
        }
    }

    private void CooldownLoader()
    {
        m_Cooldown = Mathf.Lerp(0.0f, 1.0f, m_CooldownElapsedTime);
        m_Cooldown = Mathf.Clamp(m_Cooldown, 0.0f, 1.0f);
        m_CooldownElapsedTime += Time.deltaTime * (1.0f / m_CooldownTime);
        m_ReadyToShot = m_Cooldown == 1.0f;
    }

    private void ResetCooldown()
    {
        m_Cooldown = 0.0f;
        m_CooldownElapsedTime = 0.0f;
        m_ReadyToShot = false;
    }

    private void ResetForce()
    {
        m_Force = 0.0f;
        m_PressedElapsedTime = 0.0f;
    }

    private void UpdateUI()
    {
        if (m_ForceSlider)
            m_ForceSlider.value = m_Force / m_MaxForce;

        if (m_CooldownSlider)
            m_CooldownSlider.value = m_Cooldown;
    }

    private void ShootParticle()
    {
        if (m_CannonParticle)
        {
            if (m_CannonOneParticle)
            {
                StartCoroutine(OneShoot());
            }
            else
            {
                ParticleSystem particle = Instantiate(m_CannonParticle, m_CannonPoint.position, m_CannonPoint.rotation);
                if (m_AttachParticle)
                    particle.transform.parent = m_Transforme;
            }
        }
        
        if (m_CannonAudio.clip)
            m_CannonAudio.Play();
    }


    private IEnumerator OneShoot()
    {
        for (int i = 0; i < m_MaxParticles; i++)
        {
            Instantiate(m_CannonOneParticle, m_CannonPoint.position, m_CannonPoint.rotation);
            yield return new WaitForSeconds(m_IntervalToNewParticle);
        }

        yield return null;
    }
}
       