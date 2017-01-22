using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedAndLoad : MonoBehaviour {

	private float				playerX, playerY, playerZ;
	public	GameObject			player, pv;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindWithTag ("Player").GetComponent<GameObject>();
		player.transform.position = new Vector3 (PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void save (){
		PlayerPrefs.SetFloat ("playerX", player.transform.position.x);
		PlayerPrefs.SetFloat ("playerY", player.transform.position.y);
		PlayerPrefs.SetFloat ("playerZ", player.transform.position.z);
	}

	public void Load (){
		player.transform.position = new Vector3 (PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
		SceneManager.LoadScene ("OutraDemo");
		Debug.Log (player.transform.position);
	}
}
