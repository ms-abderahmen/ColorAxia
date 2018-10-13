using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoScript : MonoBehaviour {
	void Start () {
		//StartCoroutine (Go ());
	}

	/*IEnumerator Go() {
		yield return new WaitForSeconds (1f);
		Logo.CrossFadeAlpha (255, 1f, true);
		yield return new WaitForSeconds (5f);
		Logo.CrossFadeAlpha (0, 1f, true);
		yield return new WaitForSeconds (2f);
		LoadingScreenManager.LoadScene (1);
	}*/

	void goScene(){
		LoadingScreenManager.LoadScene (1);
	}
}
