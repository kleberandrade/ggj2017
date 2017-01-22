using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;

    public static GameManager Instance
    {
        get { return m_Instance; }
    }

    private AStar[] m_Enemies;
    private Level m_Level;
    private PlayerController m_PlayerController;
    private DialogManager m_DialogManager;

    [SerializeField]
    private GameObject m_Gameover;

    [SerializeField]
    private GameObject m_Hud;

    [SerializeField]
    private AudioClip m_ThemeClip;

    [Header("GameLoop")]
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;

    private void Start()
    {
        if (m_Instance == null)
            m_Instance = this.gameObject.GetComponent<GameManager>();

        m_Gameover.SetActive(false);
        m_Level = GetComponent<Level>();

        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);


        StartCoroutine(GameLoop());
    }

    private IEnumerator RoundStarting()
    {
        Debug.Log("RoundStarting...");
        m_Level.LoadLevel();

        while (!m_Level.ready)
        {
            yield return null;
        }

        GameObject robot = GameObject.FindGameObjectWithTag("Player");
        m_PlayerController = robot.GetComponent<PlayerController>();
        m_PlayerController.Pause();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        m_Enemies = new AStar[gos.Length];
        for (int i = 0; i < gos.Length; i++)
        {
            m_Enemies[i] = gos[i].GetComponent<AStar>();
        }

        m_DialogManager = DialogManager.Instance;

        Debug.Log("Finish RoundStarting.");

        yield return m_StartWait;
    }

    private IEnumerator RoundDialoging()
    {
        ThemeAudio.Instance.ChangeMusic(m_ThemeClip, 1.0f);

        m_DialogManager.Play();

        while (m_DialogManager.IsTalking)
        {
            yield return null;
        }

        yield return null;
    }

    private IEnumerator RoundPlaying()
    {
        m_PlayerController.Resume();

        while (m_PlayerController && m_PlayerController.IsAlive)
        {
            yield return null;
        }

        yield return null;
    }

    private IEnumerator RoundEnding()
    {
        m_Hud.SetActive(false);
        m_Gameover.SetActive(true);
        yield return m_EndWait;
    }

    private IEnumerator GameLoop()
    {
        yield return null;
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundDialoging());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());
    }

    public void CreateEvent(List<Dialog> dialogues)
    {
        m_DialogManager.Register(dialogues);

        StartCoroutine(EventCreated());
    }

    private IEnumerator EventCreated()
    {
        m_PlayerController.Pause();
        m_DialogManager.Play();
        
        foreach (AStar enemy in m_Enemies)
            enemy.Pause();
        
        while (m_DialogManager.IsTalking)
        {
            yield return null;
        }

        m_PlayerController.Resume();
        foreach (AStar enemy in m_Enemies)
            enemy.Resume();

        yield return null;
    }
}