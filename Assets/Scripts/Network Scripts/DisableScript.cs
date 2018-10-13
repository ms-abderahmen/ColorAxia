using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DisableScript : NetworkBehaviour {

	void Start(){
		if (isLocalPlayer) {
			GetComponent<PlayerScriptNetwork> ().enabled = true;
		}
	}
}
