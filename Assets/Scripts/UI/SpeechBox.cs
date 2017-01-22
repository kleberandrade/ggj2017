using UnityEngine;
using UnityEngine.UI;

public class SpeechBox : MonoBehaviour
{
    [SerializeField]
    private Image m_CharacterImage;

    [SerializeField]
    private Text m_CharacterName;

    [SerializeField]
    private Text m_CharacterText;

    [SerializeField]
    private DialogText m_DialogText;

    public void Set(Sprite picture, string name)
    {
        m_CharacterText.text = string.Empty;
        m_CharacterName.text = name;
        m_CharacterImage.sprite = picture;
    }

    public void Speak(string text)
    {
        m_DialogText.ChangeText(text);
    }

    public bool IsSpeaking
    {
        get { return m_DialogText.IsSpeaking; }
    }
}
