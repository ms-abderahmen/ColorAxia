using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	
	public void ResumeGame(){
		gameObject.SetActive (false);
		Time.timeScale = 1;
	}
	public void MainMenu(){
		Time.timeScale = 1;
		LoadingScreenManager.LoadScene (1);
	}
}
