using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]
public class DialogText : MonoBehaviour
{
    private Text m_Text;

    private AudioSource m_AudioSource;

    [SerializeField]
    private float m_TimeToWrite = 0.01f;

    private bool m_SkipPressed;

    private bool m_IsSpeaking = false;

    public bool IsSpeaking
    {
        get { return m_IsSpeaking; }
    }

	private void Start ()
    {
        m_Text = GetComponent<Text>();
        m_AudioSource = GetComponent<AudioSource>();

        m_AudioSource.playOnAwake = false;
        m_AudioSource.loop = true;
	}

    public void ChangeText(string text)
    {
        m_IsSpeaking = true;
        m_SkipPressed = false;
        StartCoroutine(Writing(text));
    }

    private void Update()
    {
        if (!m_IsSpeaking)
            return;

        if (Input.anyKeyDown)
            m_SkipPressed = true;
    }

    private IEnumerator Writing(string text)
    {
        m_Text.text = string.Empty;
        m_AudioSource.Play();

        for (int i = 0; i < text.Length; i++)
        {
            if (m_SkipPressed)
                break;

            m_Text.text = text.Substring(0, i + 1);
            yield return new WaitForSeconds(m_TimeToWrite);
        }

        m_Text.text = text.Substring(0, text.Length);
        m_AudioSource.Stop();
        m_IsSpeaking = false;

        yield return null;
    }
}
