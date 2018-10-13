using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public int playerNumber;
	public StageScript stage;
	public float JumpDuration = 0.5f;
	public GameObject PauseMenu;
	public GameObject Scored;
	public Text ScoreText;

	private bool ground;
	private bool pillPowerUp = false;
	private Vector3 MoveRight;
	private Vector3 MoveDown;
	private Animation Anim;
	private TextMesh PlayerText;
	private AnimationClip clip1;
	private AnimationClip clip2;
	private int row;
	private int col;
	private bool rightButton = false;
	private bool leftButton = false;
	private bool upButton = false;
	private bool downButton = false;

	// Use this for initialization
	void Start () {
		MoveRight.Set(0,0,6);
		MoveDown.Set (6,0,0);
		Anim = gameObject.GetComponent<Animation> ();
		PlayerText = gameObject.GetComponentInChildren<TextMesh> ();
		PauseMenu.SetActive (false);
		PlayerText.text = GameManagerScript.playerName;
		row = (int) transform.position.x / 6;
		col = (int) transform.position.z / 6;
		stage.scores[playerNumber] = 0;
		ScoreText.text = GameManagerScript.playerName + " : " + stage.scores[playerNumber].ToString ();
	}

	// Update is called once per frame
	void Update () {
		//PlayerText.transform.LookAt (Camera.main.transform.position);
		//PlayerText.transform.Rotate (new Vector3 (0, 180, 0));
		/*if ((Input.GetAxisRaw ("Horizontal") == 0) && (Input.GetAxisRaw ("Vertical") == 0)) {
			button = false;
		}*/
		if (GameManagerScript.stopMoving == false) {
			if (((Input.GetAxisRaw ("Horizontal") == 1) || (rightButton == true)) && (ground == true) && (transform.position.z < 42) && (CheckFreeSpace (row, col + 1))) {
				//transform.position += MoveRight;
				ground = false;
				JumpAnimation (MoveRight);
				stage.positionMatrix [row, col + 1] = playerNumber;
				//button = true;
			}
			if (((Input.GetAxisRaw ("Horizontal") == -1) || (leftButton == true)) && (ground == true) && (transform.position.z > 0) && (CheckFreeSpace (row, col - 1))) {
				//transform.position -= MoveRight;
				ground = false;
				JumpAnimation (-MoveRight);
				stage.positionMatrix [row, col - 1] = playerNumber;
				//button = true;
			}
			if (((Input.GetAxisRaw ("Vertical") == -1) || (downButton == true)) && (ground == true) && (transform.position.x < 42) && (CheckFreeSpace (row + 1, col))) {
				//transform.position += MoveDown;
				ground = false;
				JumpAnimation (MoveDown);
				stage.positionMatrix [row + 1, col] = playerNumber;
				//button = true;
			}
			if (((Input.GetAxisRaw ("Vertical") == 1) || (upButton == true)) && (ground == true) && (transform.position.x > 0) && (CheckFreeSpace (row - 1, col))) {
				//transform.position -= MoveDown;
				ground = false;
				JumpAnimation (-MoveDown);
				stage.positionMatrix [row - 1, col] = playerNumber;
				//button = true;
			}
			if (Input.GetKeyDown (KeyCode.P)) {
				OpenPauseMenu ();
			}
		}
		//if (right == true) {
		//	  transform.position = Vector3.Lerp(transform.position,transform.position+MoveRight,Time.deltaTime);
		//}
		if (ground == true) {
			stage.stageMatrix[row,col] = playerNumber;
			stage.positionMatrix [row, col] = playerNumber;
			Change (stage.squares [(row * 8) + col].GetComponent<Renderer> (), playerNumber);
		}
	}

	void OnCollisionEnter (Collision other) {
		if (other.transform.tag == "Square") {
			Renderer rend = other.gameObject.GetComponent<Renderer> ();
			Change (rend, playerNumber);
			stage.positionMatrix [row, col] = 0;
			row = (int) other.transform.position.x / 6;
			col = (int) other.transform.position.z / 6;
			if (pillPowerUp == true) {
				PillPower ();
			}
			stage.stageMatrix[row,col] = playerNumber;
			transform.position = new Vector3 (row*6,0.75f,col*6);
			stage.positionMatrix [row, col] = playerNumber;
			stage.CheckBorders (playerNumber);
			ground = true;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Collectable")) {
			ScoreCalculation (other.transform);
			Destroy (other.gameObject);
			stage.CreateCollecatble ();
		}
		if (other.gameObject.CompareTag ("Arrow")) {
			float A = (other.transform.rotation.eulerAngles.y / 180) * Mathf.PI;
			ColorRow ((int) other.transform.position.z / 6, (int) other.transform.position.x / 6, (int)Mathf.Cos (A), (int)Mathf.Sin (A));
			Destroy (other.gameObject);
			stage.CreateArrow ();
		}
		if (other.gameObject.CompareTag ("Pill")) {
			pillPowerUp = true;
			transform.Find ("PillPower").gameObject.SetActive (true);
			StartCoroutine (PillTime ());
			Destroy (other.gameObject);
		}
	}

	//Various Functions
	IEnumerator PillTime(){
		yield return new WaitForSeconds (5f);
		pillPowerUp = false;
		transform.Find ("PillPower").gameObject.SetActive (false);
	}

	void Change(Renderer rend,int playerNumber){
		if (playerNumber == 1) {
			rend.material.color = Color.red;
		}
		if (playerNumber == 2) {
			rend.material.color = Color.green;
		}
		if (playerNumber == 3) {
			rend.material.color = Color.yellow;
		}
		if (playerNumber == 4) {
			rend.material.color = Color.blue;
		}	
	}
	void PillPower(){
		if(row-1>=0){
			Change (stage.squares [((row - 1) * 8) + col].GetComponent<Renderer> (), playerNumber);
			stage.stageMatrix[row-1,col] = playerNumber;
		}
		if(row+1<8){
			Change (stage.squares [((row + 1) * 8) + col].GetComponent<Renderer> (), playerNumber);
			stage.stageMatrix[row+1,col] = playerNumber;
		}
		if(col-1>=0){
			Change (stage.squares [(row * 8) + (col - 1)].GetComponent<Renderer> (), playerNumber);
			stage.stageMatrix[row,col-1] = playerNumber;
		}
		if(col+1<8){
			Change (stage.squares [(row * 8) + (col + 1)].GetComponent<Renderer> (), playerNumber);
			stage.stageMatrix[row,col+1] = playerNumber;
		}
	}

	void JumpAnimation (Vector3 JumpDirection) {
		clip1 = new AnimationClip ();
		clip1.legacy = true;
		clip2 = new AnimationClip ();
		clip2.legacy = true;
		clip1.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurve.Linear(0,transform.position.z,JumpDuration/2,transform.position.z+(JumpDirection.z/2)));
		clip1.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurve.EaseInOut(0, 0.8f, JumpDuration/2, 5f));
		clip1.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurve.Linear(0,transform.position.x,JumpDuration/2,transform.position.x+(JumpDirection.x/2)));
		clip2.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurve.Linear(0,transform.position.z+(JumpDirection.z/2),JumpDuration/2,transform.position.z+JumpDirection.z));
		clip2.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurve.EaseInOut(0, 5f, JumpDuration/2, 0.8f));
		clip2.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurve.Linear(0,transform.position.x+(JumpDirection.x/2),JumpDuration/2,transform.position.x+JumpDirection.x));
		Anim.AddClip(clip1, "jumppart1");
		Anim.AddClip(clip2, "jumppart2");
		Anim.Play("jumppart1");
		Anim.PlayQueued("jumppart2",QueueMode.CompleteOthers);
	}

	void ScoreCalculation(Transform pos){
		int count = 0;
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (stage.stageMatrix [i, j] == playerNumber) {
					count++;
					Renderer rend = stage.squares [(i * 8) + j].GetComponent<Renderer> ();
					rend.material.color = Color.white;
					stage.stageMatrix [i, j] = 0;
				}
			}
		}
		//Score text script
		GameObject clone = (GameObject)Instantiate (Scored, pos.position, Quaternion.Euler (0, -90, 0));
		Vector3 vect = clone.transform.position;
		vect.y = 4f;
		clone.transform.position = vect;
		TextMesh TheScore = clone.transform.Find ("Score").GetComponent<TextMesh> ();
		TheScore.color = Color.red;
		TheScore.text = count.ToString ();
		Destroy (clone,3f);
		//end of score test script
		stage.scores[playerNumber] += count;
		ScoreText.text = GameManagerScript.playerName + " : " + stage.scores[playerNumber].ToString ();
	}

	void ColorRow (int Col,int Row,int xdirection,int ydirection) {
		int xactualPos = Col;
		int yactualPos = Row;
		while ((xactualPos >= 0) && (xactualPos < 8) && (yactualPos >= 0) && (yactualPos < 8)) {
			stage.stageMatrix [yactualPos, xactualPos] = playerNumber;
			Renderer rend = stage.squares [(yactualPos * 8) + xactualPos].GetComponent<Renderer> ();
			Change (rend, playerNumber);
			xactualPos += xdirection;
			yactualPos += ydirection;
		}
	}

	bool CheckFreeSpace(int Row,int Col){
		if ((Row > 7) || (Row < 0) || (Col > 7) || (Col < 0))
			return false;
		for (int i = 1; i <= 4; i++) {
			if (i == playerNumber)
				continue;
			if (stage.positionMatrix [Row, Col] == i)
				return false;
		}
		return true;
	}

	//functions for DPad Controls
	public void RightPress(){
		rightButton = true;
	}
	public void RightRelease(){
		rightButton = false;
	}

	public void LeftPress(){
		leftButton = true;
	}
	public void LeftRelease(){
		leftButton = false;
	}

	public void UpPress(){
		upButton = true;
	}
	public void UpRelease(){
		upButton = false;
	}

	public void DownPress(){
		downButton = true;
	}
	public void DownRelease(){
		downButton = false;
	}

	public void OpenPauseMenu () {
		Animator PauseAnim = PauseMenu.GetComponent<Animator> ();
		if (Time.timeScale == 0) {
			PauseMenu.SetActive (false);
			Time.timeScale = 1;
		} else {
			PauseAnim.Play ("PauseOpenMenu");
			PauseMenu.SetActive (true);
			Time.timeScale = 0;
		}
	}
}
