using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {
	Animation anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation> ();
		anim.PlayQueued ("CameraTranlation", QueueMode.PlayNow,PlayMode.StopAll);
		anim.PlayQueued ("CameraTranslation2", QueueMode.CompleteOthers,PlayMode.StopAll);
		anim.PlayQueued ("CameraTranslation3", QueueMode.CompleteOthers,PlayMode.StopAll);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
