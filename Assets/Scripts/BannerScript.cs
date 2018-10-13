using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BannerScript : MonoBehaviour {

	public PlayerScriptTuto player;
	public StageScriptTuto stage;

	private Text BannerText;
	private Animator Animmm;

	void Start () {
		Animmm = GetComponent<Animator> ();
		BannerText = transform.GetChild (0).GetComponent<Text> ();
		StartCoroutine (StartTutorial ());
		GameManagerScript.stopMoving = true;
	}
	
	void Update () {
		
	}

	IEnumerator StartTutorial(){
		yield return new WaitForSeconds (1.5f);
		BannerText.text = "Hi, Welcome to ColorAxia ! Since you're new to the game , we're here to help you to understand the basics so you can fully enjoy it :)";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (6f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Let's start by moving the character around. Use the arrow keys to move your character in the four different directions !";
		GameManagerScript.stopMoving = false;
		Animmm.Play ("BannerAnimation");
		yield return new WaitUntil (() => player.doneMoving [4]);
		player.showText (player.gameObject.transform, "Good !", Color.blue);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Good job ! Now as you can see, when you move your character from one square to another, its color changes to red, which is your color";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (6f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "If you're wondering why, go collect that collectable object that just spawned in the stage and see what happens ! To collect it just move to it";
		stage.CreateCollecatble ();
		Animmm.Play ("BannerAnimation");
		player.collected = false;
		yield return new WaitUntil (() => player.collected);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Good ! The colored squares went back to normal state and a number appeared above you";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (5f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "The number shows how many squares you actually colored when you collect those collectables and add that number to your score";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (7f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "So, as you may have guessed, your main goal is to get the best score before the time runs out, it's that simple";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (6f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Now, let me introduce you to a new collectable object! Try collecting that moving arrow that just spawned in the stage and see what happens";
		stage.CreateArrow ();
		Animmm.Play ("BannerAnimation");
		player.collected = false;
		yield return new WaitUntil (() => player.collected);
		player.showText (player.gameObject.transform, "Excellent !", Color.blue);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Good job! As you may have noticed, some squares were colored according to the direction of the arrow the moment you touched it, and that's exactly what this arrow does";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (9f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Now, here is another interesting object! Do you see that moving pill on the stage? Go get it and see what happens";
		stage.CreatePill ();
		Animmm.Play ("BannerAnimation");
		player.collected = false;
		yield return new WaitUntil (() => player.collected);
		player.showText (player.gameObject.transform, "Power UP!", Color.blue);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Yup, this is a power UP object! Moving after collecting this will make you color the adjacent squares too. But it only lasts for 5 seconds, so be fast!";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (8f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);

		BannerText.text = "Well, that's it! You now know the basics of ColorAxia. We hope you enjoy our game, and have fun :)";
		Animmm.Play ("BannerAnimation");
		yield return new WaitForSeconds (5f);
		Animmm.Play ("BannerOutAnimation");
		yield return new WaitForSeconds (1f);
		GameManagerScript.stopMoving = true;
		GameManagerScript.stopTimer = true;
		PlayerPrefs.SetInt ("tuto", 1);
		/*Social.ReportProgress (ColorAxiaResources.achievement_apprentice, 100.0f, (bool succes) => 
			{
				
			});*/
		LoadingScreenManager.LoadScene (4);
	}
}
