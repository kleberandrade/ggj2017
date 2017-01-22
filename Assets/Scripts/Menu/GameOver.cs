using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public	 GameObject		menu;

	private  AStar 			AStar;

	// Use this for initialization
	void Start () {
		AStar = FindObjectOfType (typeof(AStar)) as AStar;
		menu = GameObject.Find ("Panel");
		menu.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		deadPlayer ();
	}

	void deadPlayer () {
		if (Time.timeScale == 1 && AStar.Dead) {
			Time.timeScale = 0;
			menu.SetActive (true);
		} else {
			Time.timeScale = 1;
			menu.SetActive (false);
		}
	}

	void gameOver () {
		
	}
}
