using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LoadingSceneAnyButton : MonoBehaviour
{
    [SerializeField]
    private string m_SceneName;

    private bool m_ButtonPressed = false;

    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update ()
    {
		if (!m_ButtonPressed && m_SceneName != string.Empty && Input.anyKeyDown)
        {
            m_ButtonPressed = true;
            m_AudioSource.Play();
            SceneManager.LoadScene(m_SceneName);
        }
	}
}
