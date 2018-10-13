using UnityEngine;
using System.Collections;

public class PillRotation : MonoBehaviour {
	public float rotationSpeed = 0.5f;

	private float x = 0f;
	private float z = 0f;

	void Update () {
		x += rotationSpeed;
		z -= rotationSpeed;
		transform.rotation = Quaternion.Euler (x,0f,z);
	}
}
