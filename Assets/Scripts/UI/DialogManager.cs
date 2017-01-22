using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private List<string> m_CharacterNames;

    [SerializeField]
    private List<Sprite> m_CharacterPictures;

    [SerializeField]
    private SpeechBox m_Speech;

    [SerializeField]
    private Text m_KeyText;

    private List<Dialog> m_Dialogues = new List<Dialog>();

    private static DialogManager m_Instance = null;

    private CanvasGroup m_CanvasGroup;

    [SerializeField]
    private float m_TimeToAppear = 0.3f;

    private float m_ElapsedTime;

    private bool m_IsTalking = false;

    private bool m_Next = false;

    public static DialogManager Instance
    {
        get { return m_Instance; }
    }

    public bool IsTalking
    {
        get { return m_IsTalking; }
    }

	private void Start ()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

	public void Register(Dialog dialog) {
        m_Dialogues.Add(dialog);
	}

    public void Register(List<Dialog> dialogues)
    {
        m_Dialogues = dialogues;
    }

    public void Clear()
    {
        m_Dialogues.Clear();
    }

    public void Play()
    {
        StartCoroutine(Talking());
	}

    private void Update()
    {
        m_ElapsedTime += Time.deltaTime;

        if (m_Speech.IsSpeaking)
            return;

        if (Input.anyKeyDown)
            m_Next = true;
    }

    private IEnumerator Talking()
    {
        m_IsTalking = true;

        while (m_Dialogues.Count > 0)
        {
            m_KeyText.text = "Skip";

            // Pegar um dialogo
            Dialog dialog = m_Dialogues[0];
            m_Dialogues.RemoveAt(0);
            m_Speech.Set(m_CharacterPictures[dialog.CharacterIndex], m_CharacterNames[dialog.CharacterIndex]);

            // Aparecer o Canvas
            m_ElapsedTime = 0.0f;
            m_CanvasGroup.alpha = 0.0f;
            while (m_ElapsedTime < m_TimeToAppear)
            {
                m_CanvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, m_ElapsedTime);
                yield return null;
            }

            m_CanvasGroup.alpha = 1.0f;
            yield return new WaitForSeconds(0.2f);

            // Conversar
            m_Next = false;
            m_Speech.Speak(dialog.Text);
            yield return new WaitForSeconds(0.1f);

            // Aguarda a conversa
            while (m_Speech.IsSpeaking)
            {
                yield return null;
            }

            yield return null;

            // Aguarda o player pressionar um botão
            m_KeyText.text = "Next";
            while (!m_Next)
            {
                yield return null;
            }
            m_Next = false;

            // Desaparecer
            m_ElapsedTime = 0.0f;
            while (m_ElapsedTime < m_TimeToAppear)
            {
                m_CanvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, m_ElapsedTime);
                yield return null;
            }

            m_CanvasGroup.alpha = 0.0f;

            yield return new WaitForSeconds(0.1f);
        }

        m_IsTalking = false;

        yield return null;
    }
}