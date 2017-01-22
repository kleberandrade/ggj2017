using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneContinue : MonoBehaviour
{ 
	private void Start ()
    {
        Button button = GetComponent<Button>();
        button.interactable = PlayerPrefs.HasKey("GameSaved");
	}
}
