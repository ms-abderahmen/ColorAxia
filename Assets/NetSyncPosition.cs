using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetSyncPosition : NetworkBehaviour {
	[SyncVar]
	public Vector3 syncPos;

	public float speed = 15f;

	void Start(){
		if(!isServer)
			CmdUpdatePos ();
		else
			syncPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {
			print ("hello");
			if(!isServer)
				CmdUpdatePos ();
			else
				syncPos = transform.position;
			return;
		} else {
			print ("hi");
			print (transform.position + " " + syncPos);
			transform.position = Vector3.Lerp (transform.position, syncPos,Time.deltaTime * speed);
		}
	}

	[Command]
	public void CmdUpdatePos(){
		syncPos = transform.position;
	}
}
