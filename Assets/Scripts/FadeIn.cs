using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {
	public Image fadeOverlay;
	public float fadeDuration = 0.25f;

	void Start () {
		StartCoroutine (FadeInn ());
	}

	IEnumerator FadeInn() {
		fadeOverlay.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
		yield return new WaitForSeconds (fadeDuration);
		fadeOverlay.gameObject.SetActive (false);
	}
}
