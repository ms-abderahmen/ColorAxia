using UnityEngine;
using System.Collections;

public class spinning : MonoBehaviour {
	public float speed = 0.2f;
	private float angle;
	// Use this for initialization
	void Start () {
		angle = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		angle += speed;
		transform.rotation = Quaternion.Euler (new Vector3 (-90f, angle, 0));
	}
}
