using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIScript : MonoBehaviour {

	public int playerNumber;
	public StageScript stage;
	public float JumpDuration = 0.5f;
	public Text ScoreText;
	public GameObject Scored;
	[HideInInspector]
	public int pos = 0;

	private bool ground;
	private bool pillPowerUp = false;
	private Vector3 MoveRight;
	private Vector3 MoveDown;
	private Animation Anim;
	private AnimationClip clip1;
	private AnimationClip clip2;
	private int row;
	private int col;
	private int[,] Collectables = new int[10, 2];
	private int collectableRow;
	private int collectableCol;
	private int previousRow;
	private int previousCol;

	// Use this for initialization
	void Start () {
		MoveRight.Set(0,0,6);
		MoveDown.Set (6,0,0);
		Anim = gameObject.GetComponent<Animation> ();
		row = (int) transform.position.x / 6;
		col = (int) transform.position.z / 6;
		stage.scores[playerNumber] = 0;
		ScoreText.text = "Player "+playerNumber+ " : " + stage.scores[playerNumber].ToString ();
		//FindCollectable ();
		//PathToCollectable ();
	}

	void Update(){
		if ((ground == true) && (GameManagerScript.stopMoving == false)) {
			FindCollectable ();
			PathToCollectable ();
		}
	}

	void OnCollisionEnter (Collision other) {
		if (other.transform.tag == "Square") {
			Renderer rend = other.gameObject.GetComponent<Renderer> ();
			Change (rend, playerNumber);
			previousCol = col;
			previousRow = row;
			stage.positionMatrix [row, col] = 0;
			row = (int) other.transform.position.x / 6;
			col = (int) other.transform.position.z / 6;
			if (pillPowerUp == true) {
				PillPower ();
			}
			transform.position = new Vector3 (row*6,0.75f,col*6);
			stage.stageMatrix[row,col] = playerNumber;
			stage.positionMatrix [row, col] = playerNumber;
			ground = true;
			stage.CheckBorders (playerNumber);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Collectable")) {
			ScoreCalculation ("Player "+playerNumber.ToString(),other.transform);
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
			rend.material.color = Color.red ;
		}
		if (playerNumber == 2) {
			rend.material.color = Color.green ;
		}
		if (playerNumber == 3) {
			rend.material.color = Color.yellow ;
		}
		if (playerNumber == 4) {
			rend.material.color = Color.blue ;
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

	void FindCollectable(){
		pos = 0;
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if ((stage.positionMatrix [i, j] == 5) || (stage.positionMatrix [i, j] == 6) || (stage.positionMatrix [i, j] == 7)) {
					Collectables [pos, 0] = i;
					Collectables [pos, 1] = j;
					pos++;
				}
			}
		}
	}

	float distance (int endRow,int endCol,int startRow,int startCol){
		return Mathf.Sqrt (Mathf.Pow (endRow - startRow, 2) + Mathf.Pow (endCol - startCol, 2));
	}

	void PathToCollectable(){
		float dist = 2000f;
		int nextRow = 0;
		int nextCol = 0;
		for (int i = 0; i < Collectables.Length/2; i++) {
			collectableRow = Collectables [i, 0];
			collectableCol = Collectables [i, 1];
			if ((distance (collectableRow, collectableCol, row + 1, col) < dist) && (CheckFreeSpace (row + 1, col)) && ((row+1 != previousRow) || (col != previousCol))) {
				nextCol = 0;
				nextRow = 1;
				dist = distance (collectableRow, collectableCol, row + 1, col);
			}
			if ((distance (collectableRow, collectableCol, row - 1, col) < dist) && (CheckFreeSpace (row - 1, col)) && ((row-1 != previousRow) || (col != previousCol))) {
				nextCol = 0;
				nextRow = -1;
				dist = distance (collectableRow, collectableCol, row - 1, col);
			}
			if ((distance (collectableRow, collectableCol, row, col + 1) < dist) && (CheckFreeSpace (row, col + 1)) && ((row != previousRow) || (col+1 != previousCol))) {
				nextCol = 1;
				nextRow = 0;
				dist = distance (collectableRow, collectableCol, row, col + 1);
			}
			if ((distance (collectableRow, collectableCol, row, col - 1) < dist) && (CheckFreeSpace (row, col - 1)) && ((row != previousRow) || (col-1 != previousCol))) {
				nextCol = -1;
				nextRow = 0;
				dist = distance (collectableRow, collectableCol, row, col - 1);
			}
		}
		stage.positionMatrix [row + nextRow, col + nextCol] = playerNumber;
		MoveToPosition (nextRow, nextCol);
	}

	void MoveToPosition(int targetRow,int targetCol){
		ground = false;
		JumpAnimation (new Vector3 (targetRow * 6, 0, targetCol * 6));
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

	void ScoreCalculation(string playerName,Transform pos){
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
		Color cc = new Color ();
		if (playerNumber == 2) {
			cc = Color.green ;
		}
		if (playerNumber == 3) {
			cc = Color.yellow ;
		}
		if (playerNumber == 4) {
			cc = Color.blue ;
		}
		TheScore.color = cc;
		TheScore.text = count.ToString ();
		Destroy (clone,3f);
		//end of score test script
		stage.scores[playerNumber] += count;
		ScoreText.text = playerName+ " : " + stage.scores[playerNumber].ToString ();
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
}
