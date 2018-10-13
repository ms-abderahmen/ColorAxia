using UnityEngine;
using System.Collections;

public class mouvement : MonoBehaviour {

	public float h = 0.1f;
	// Use this for initialization
	void Start () {
		Vector3 t = transform.position;
		t.x = -5;
		transform.position = t;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (h, 0, 0));
	}
}
