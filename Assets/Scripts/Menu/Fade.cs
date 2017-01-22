using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public 		RawImage 	img;
    private		bool 		FadeOut;
    private		CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        img = GameObject.Find("BlackFade").GetComponent<RawImage>();
        FadeOut = false;
        //canvasGroup.alpha = 0;
        FadeInOut();
		Debug.Log (Time.timeScale);
    }

    /*void Update()
    {
        if (Input.GetButtonDown("Submit"))
            FadeInOut();
    }*/

	public void FadeInOut()
    {
        StartCoroutine("Fading");
    }

    IEnumerator Fading()
    {
		Time.timeScale = 1;
		if (!FadeOut)
        {
			img.CrossFadeAlpha(0.0f, 2.0f, false);
			FadeOut = true;
			yield return new WaitForSeconds (1.8f);
			img.gameObject.SetActive (false);
        }
        else
        {
			img.gameObject.SetActive(true);
			img.canvasRenderer.SetAlpha (0.0f);
			img.CrossFadeAlpha(1.0f, 2.0f, false);
			FadeOut = false;
			yield return null;
        }
        
    }
}
