using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class buttons : MonoBehaviour {

	public	GameObject 		group;
	public	EventSystem 	eventSystem;
	public	GameObject 		selectedButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine (loadScene("OutraDemo"));
	}

	//função para iniciar o jogo
	public void play(){
		SceneManager.LoadScene ("Loading");
	}

	//função para retornar ao jogo
	public void retry(){
		SceneManager.LoadScene ("OutraDemo");
	}

	//função para retornar ao menu principal
	public void menuPrincipal(){
		SceneManager.LoadScene ("MenuDemo");
	}

	//função para sair do jogo
	public void quitGame(){
		Application.Quit ();
	}

	//função para entrar no menu de créditos
	public void Credits(){
		SceneManager.LoadScene ("Credits");
	}

	//função para entrar do título para menu principal
	public void LoadByIndexTitle(int sceneIndex){
		eventSystem.SetSelectedGameObject (selectedButton);
		if (Input.GetButtonDown ("Submit")){
			SceneManager.LoadScene (sceneIndex);
		}
	}
}
