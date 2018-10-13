using UnityEngine;
using System.Collections;

public class ArrowRotation : MonoBehaviour {

	public float duration = 2f;

	private int angle = 0;

	// Use this for initialization
	void Start () {
		angle = 90 * ((int)(Random.Range (0f, 3f)));
		StartCoroutine (ARotation (duration));
	}

	IEnumerator ARotation(float duration){
		while (true) {
			transform.rotation = Quaternion.Euler (0, angle, 0);
			angle += 90;
			angle = angle % 360;
			yield return new WaitForSeconds (duration);
		}
	}
}
