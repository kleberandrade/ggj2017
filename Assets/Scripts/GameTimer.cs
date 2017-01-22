using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private Text m_Text;

	private void Start ()
    {
        m_Text = GetComponent<Text>();
	}
	
	private void Update ()
    {
        m_Text.text = ((int) Time.timeSinceLevelLoad).ToString();
	}
}
