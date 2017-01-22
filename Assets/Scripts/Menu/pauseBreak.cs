using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseBreak : MonoBehaviour {

	public		GameObject	menu;

	// Use this for initialization
	void Start () {
		menu = GameObject.Find ("Panel");
		menu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			pauseGame ();
		}
	}

	public void pauseGame () {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			menu.SetActive (true);
		} else {
			Time.timeScale = 1;
			menu.SetActive (false);
		}
	}
}
