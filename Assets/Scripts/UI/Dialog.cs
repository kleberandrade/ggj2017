using UnityEngine;

public class Dialog : MonoBehaviour
{
    public int CharacterIndex { get; set; }

    public string Text { get; set; }

    public Dialog(int index, string dialogText)
    {
        CharacterIndex = index;
        Text = dialogText;
    }
}
