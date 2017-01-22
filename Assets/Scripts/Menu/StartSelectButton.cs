using UnityEngine;
using UnityEngine.EventSystems;

public class StartSelectButton : MonoBehaviour
{
    [SerializeField]
    private EventSystem m_EventSystem;

    [SerializeField]
    private GameObject m_SelectedButton;

    private bool m_ButtonSelected;

    private void Start()
    {
        if (m_EventSystem)
            m_EventSystem.SetSelectedGameObject(m_SelectedButton);
    }

    private void OnEnable()
    {
        if (m_EventSystem)
                m_EventSystem.SetSelectedGameObject(m_SelectedButton);
    }
}
