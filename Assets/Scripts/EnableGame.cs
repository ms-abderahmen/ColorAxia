using UnityEngine;
using System.Collections;

public class EnableGame : MonoBehaviour {

	public void StartGame(){
		GameManagerScript.stopMoving = false;
		GameManagerScript.stopTimer = false;
		gameObject.SetActive (false);
	}
}
