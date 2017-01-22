using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
public class ThemeAudio : MonoBehaviour
{
    private static ThemeAudio m_Instance = null;

    private static bool m_ApplicationIsQuitting = false;

    private AudioSource m_AudioSource;

    private void Awake ()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.ignoreListenerVolume = true;
    }

    public static ThemeAudio Instance
    {
        get { return m_Instance; }
    }
	
	public void ChangeMusic (AudioClip clip, float time)
    {
        StartCoroutine(Crossing(clip, time));
	}

    private IEnumerator Crossing(AudioClip clip, float time)
    {
        float elapsedTime = 0.0f;
        float volume = m_AudioSource.volume;
        
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            m_AudioSource.volume = Mathf.Lerp(1.0f, 0.0f, elapsedTime);
            yield return null;
        }

        elapsedTime = 0.0f;
        m_AudioSource.clip = clip;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            m_AudioSource.volume = Mathf.Lerp(0.0f, 1.0f, elapsedTime);
            yield return null;
        }

        yield return null;
    }

    private void OnDestroy()
    {
        m_ApplicationIsQuitting = true;
    }
}
