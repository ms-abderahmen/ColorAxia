using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public float LevelTime;
	public Text TimerText;
	public StageScript stage;
	public GameObject winmenu;
	public RawImage WinText;
	public RawImage DrawText;

	private string winner;
	// Use this for initialization
	void Start () {
		//AdManager.Instance.RequestInterstitial ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManagerScript.stopTimer == false) {
			LevelTime -= Time.deltaTime;
			if (LevelTime <= 0) {
				int maxScore = 0;
				LevelTime = 0;
				for (int i = 1; i < 5; i++) {
					if (stage.scores [i] > maxScore) {
						maxScore = stage.scores [i];
						winner = i.ToString ();
					} else if (stage.scores [i] == maxScore) {
						winner = "Draw";
					}
				}
				GameManagerScript.stopMoving = true;
				GameManagerScript.stopTimer = true;
				winmenu.SetActive (true);
				Animator MenuAnim = winmenu.GetComponent<Animator> ();
				if (winner != "Draw") {
					WinText.gameObject.SetActive (true);
					DrawText.gameObject.SetActive (false);
					if (winner == "1") {
						WinText.rectTransform.localPosition = new Vector3 (-70, 140);
						Social.ReportProgress (ColorAxiaResources.achievement_warrior, 100.0f, (bool succes) => 
							{

							});
						GameManagerScript.wincount++;
						GameManagerScript.losecount = 0;
						if (GameManagerScript.wincount == 5) {
							Social.ReportProgress (ColorAxiaResources.achievement_gladiator, 100.0f, (bool succes) => 
								{

								});
						}
					}
					if (winner == "2") {
						WinText.rectTransform.localPosition = new Vector3 (70, 140);
						GameManagerScript.losecount++;
						GameManagerScript.wincount = 0;
						if (GameManagerScript.losecount == 3) {
							Social.ReportProgress (ColorAxiaResources.achievement_mr_turtle, 100.0f, (bool succes) => 
								{

								});
						}
						if (GameManagerScript.losecount == 10) {
							Social.ReportProgress (ColorAxiaResources.achievement_alien, 100.0f, (bool succes) => 
								{

								});
						}
					}
					if (winner == "3") {
						WinText.rectTransform.localPosition = new Vector3 (-70, 0);
						GameManagerScript.losecount++;
						GameManagerScript.wincount = 0;
						if (GameManagerScript.losecount == 3) {
							Social.ReportProgress (ColorAxiaResources.achievement_mr_turtle, 100.0f, (bool succes) => 
								{

								});
						}
						if (GameManagerScript.losecount == 10) {
							Social.ReportProgress (ColorAxiaResources.achievement_alien, 100.0f, (bool succes) => 
								{

								});
						}
					}
					if (winner == "4") {
						WinText.rectTransform.localPosition = new Vector3 (70, 0);
						GameManagerScript.losecount++;
						GameManagerScript.wincount = 0;
						if (GameManagerScript.losecount == 3) {
							Social.ReportProgress (ColorAxiaResources.achievement_mr_turtle, 100.0f, (bool succes) => 
								{

								});
						}
						if (GameManagerScript.losecount == 10) {
							Social.ReportProgress (ColorAxiaResources.achievement_alien, 100.0f, (bool succes) => 
								{

								});
						}
					}
				} else {
					WinText.gameObject.SetActive (false);
					DrawText.gameObject.SetActive (true);
				}
				MenuAnim.Play ("PauseOpenMenu");
				//AdManager.Instance.showInter ();

			}
			TimerText.text = string.Format ("{0:0}:{1:00}", Mathf.Floor((LevelTime / 60)), Mathf.Floor((LevelTime % 60)));
		}
	}

	public void gotoMainMenu (){
		LoadingScreenManager.LoadScene (1);
	}
}
