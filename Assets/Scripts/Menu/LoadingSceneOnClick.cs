using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LoadingSceneOnClick : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField]
    private bool m_Pressed;

    [SerializeField]
    private float m_Interval = 1.0f;

    private float m_ElapsedTime = 0.0f;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.loop = false;
        m_AudioSource.playOnAwake = false;
    }

    private void Update()
    {
        m_ElapsedTime += Time.time;
        if (m_Pressed)
        {
            m_ElapsedTime = 0.0f;
            m_Pressed = false;
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        if (m_Pressed)
            return;

        if (m_ElapsedTime < m_Interval)
            return;

        m_Pressed = true;
        m_AudioSource.Play();
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadByName(string sceneName)
    {
        if (m_Pressed)
            return;

        if (m_ElapsedTime < m_Interval)
            return;

        m_Pressed = true;

        m_AudioSource.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadBySceneLoaded()
    {
        if (m_Pressed)
            return;

        if (m_ElapsedTime < m_Interval)
            return;

        m_Pressed = true;

        m_AudioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        if (m_Pressed)
            return;

        if (m_ElapsedTime < m_Interval)
            return;

        m_Pressed = true;
        m_AudioSource.Play();

        m_Interval = 2.0f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}