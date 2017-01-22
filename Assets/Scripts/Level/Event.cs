using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour {

	private bool done = false;
	public List<Dialog> dialogues;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)//Entrando em colisão com as ondas ele se mexe
	{
		if (!done && col.gameObject.tag == "Particle") {
			done = true;
			GameManager.Instance.CreateEvent (dialogues);
		}
	}
}
