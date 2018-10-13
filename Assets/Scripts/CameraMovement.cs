using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public Transform target;
	public float smoothing = 5f;
	Vector3 offset;
	Vector3 finalPos;
	Vector3 MouseWheel;

	// Use this for initialization
	void Start () {
		offset.Set (17.5f,19f,15.5f);
		MouseWheel.Set (-2f, -2f, -2f);
		transform.position = target.position+offset;
	}
	
	// Update is called once per frame
	void Update () {
		finalPos = target.position + offset;
		finalPos.y = 0.75f+offset.y;
		transform.position = Vector3.Lerp (transform.position, finalPos, smoothing * Time.deltaTime);
		if ((Input.GetAxis ("Mouse ScrollWheel") > 0) && (offset != new Vector3(17.5f,19f,15.5f)))  {
			offset += MouseWheel;
		} else if ((Input.GetAxis ("Mouse ScrollWheel") < 0) && (offset != new Vector3(31.5f,33f,29.5f))) {
			offset -= MouseWheel;
		}
	}
}
