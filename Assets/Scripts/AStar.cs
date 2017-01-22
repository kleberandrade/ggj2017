using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
	public GameObject Target;
	public float timeToAct;
	public int anglesPerSecond;
	private int walk = 0;
	private bool paused = false;

    public static bool Dead = false;
	
	// Update is called once per frame
	void Update ()
    {
		if (walk > 0 && !paused) {
			MovingToPlayer ();
		}
    }

    void OnTriggerEnter2D (Collider2D col)//Entrando em colisão com as ondas ele se mexe
    {
		if (col.gameObject.tag == "Particle") {
			walk++;
			StartCoroutine ("waiting");
		}
    }

    void OnCollisionEnter2D (Collision2D col)
    {
		if (col.gameObject.tag == "Player") 
		{
			Dead = true;
			col.gameObject.GetComponent<PlayerController>().Die();
		}
    }

    void MovingToPlayer()
    {
        Vector3 rot = this.transform.rotation.eulerAngles;
        Quaternion quat;
		Vector2 direction = new Vector2 (Target.transform.position.x - this.transform.position.x, Target.transform.position.y - this.transform.position.y);
		direction.Normalize ();
		float angle = -90 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rot.z;
        if (Mathf.Abs(angle) > 180)
        {
            angle = -Mathf.Sign(angle) * (360 - Mathf.Abs(angle));
        }
        rot.z += Mathf.Sign(angle) * Mathf.Min(Time.deltaTime * anglesPerSecond, Mathf.Abs(angle));
        quat = this.transform.rotation;
        quat.eulerAngles = rot;
        this.transform.rotation = quat;
		this.transform.Translate(direction.x * Time.deltaTime / timeToAct, direction.y * Time.deltaTime / timeToAct, 0);
    }

	IEnumerator waiting()
	{
		yield return new WaitForSeconds (3.0f);
		walk--;
	}

	public void Pause() {
		this.paused = true;
	}

	public void Resume() {
		this.paused = false;
	}
}
