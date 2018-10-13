using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
//using Facebook.Unity;
using System.Collections;
//using GooglePlayGames;

public class GameManagerScript : MonoBehaviour {
	public static string playerName;
	public static bool stopMoving = false;
	public static bool stopTimer = false;
	public static int wincount = 0;
	public static int losecount = 0;
	public InputField TextField;

	private string savedName;
	private bool IsConnectedToGoogleServices = false;

	void Awake () {
		DontDestroyOnLoad (gameObject);
		savedName = PlayerPrefs.GetString ("Player Name");
		TextField.text = savedName;
		//facebook
		/*if (FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			//Handle FB.Init
			FB.Init( () => {
				FB.ActivateApp();
			});
		}*/

		//PlayGamesPlatform.Activate ();
		//ConnectToGoogle ();
	}

	public void StartGame ()
	{
		//AdManager.Instance.HideAd ();
		stopMoving = true;
		stopTimer = true;
		if (PlayerPrefs.HasKey ("tuto")) {
			LoadingScreenManager.LoadScene (4);
		} else {
			LoadingScreenManager.LoadScene (3);
		}
	}
	public void EditName(){
		if (TextField.text != "") {
			PlayerPrefs.SetString ("Player Name", TextField.text);
			playerName = TextField.text;
		} else {
			playerName = "Player 1";
		}
	}

	public void Exit(){
		Application.Quit ();
	}

	public void ClearTuto(){
		PlayerPrefs.DeleteKey ("tuto");
	}

	void OnApplicationPause (bool pauseStatus)
	{
		// Check the pauseStatus to see if we are in the foreground
		// or background
		if (!pauseStatus) {
			//app resume
			/*if (FB.IsInitialized) {
				FB.ActivateApp();
			} else {
				//Handle FB.Init
				FB.Init( () => {
					FB.ActivateApp();
				});
			}*/
		}
	}
	
	public bool ConnectToGoogle()
	{
		if (!IsConnectedToGoogleServices) {
			Social.localUser.Authenticate ((bool succes) => {
				IsConnectedToGoogleServices = succes;
			});
		}
		return IsConnectedToGoogleServices;
	}

	public void ToAchievement(){
		if (Social.localUser.authenticated) {
			Social.ShowAchievementsUI ();
		}
	}

	public void ConnectButton(){
		Social.localUser.Authenticate ((bool succes) => {
			IsConnectedToGoogleServices = succes;
		});
	}
}
