using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {
	private float angle;
	public float speed = 10f;
	private float x;
	private float z;
	// Use this for initialization
	void Start () {
		angle = 0f;
		//AdManager.Instance.RequestBanner ();
	}
	
	// Update is called once per frame
	void Update () { 
		x = 21.5f - 33.5f * Mathf.Sin (angle);
		z = 21.5f - 33.5f * Mathf.Cos (angle);
		transform.position = new Vector3(x,30f,z);
		transform.rotation = Quaternion.Euler (45, (angle * 180) / (Mathf.PI), 0);
		angle += (2 * Time.deltaTime * Mathf.PI) / speed;

	}
}
