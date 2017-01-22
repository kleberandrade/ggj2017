using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	/*[SerializeField]
	private Text loadingText;*/

	public  string 	startingSceneName;

	public  Text   	loadingText;
	private bool 	loadscene;

	void Update(){
		loadingText.color = new Color (loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong (Time.time, 1));
		loadingText.text = "Loading...";
	}

	private IEnumerator Start() {
		yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));
	}

	private IEnumerator LoadSceneAndSetActive (string sceneName){
		if (loadscene) {
			Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);
			SceneManager.SetActiveScene (newlyLoadedScene);
		}
		yield return SceneManager.LoadSceneAsync (sceneName);
	}
}

