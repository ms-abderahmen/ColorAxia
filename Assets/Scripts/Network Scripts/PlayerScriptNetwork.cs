using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScriptNetwork : NetworkBehaviour {
	public int playerNumber;
	public float JumpDuration = 0.5f;
	public GameObject PauseMenu;
	public GameObject Scored;
	public Text bug;
	//public Text ScoreText;

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
		//------------------
		MoveRight.Set(0,0,6);
		MoveDown.Set (6,0,0);
		Anim = gameObject.GetComponent<Animation> ();
		PlayerText = gameObject.GetComponentInChildren<TextMesh> ();
		PauseMenu = GameObject.FindGameObjectWithTag ("ShowOnPause");
		bug = GameObject.FindGameObjectWithTag ("bug").GetComponent<Text> ();
		if (isServer) {
			//ScoreText = GameObject.Find ("ScoreTextP1").GetComponent<Text> ();
			playerNumber = 1;
			GetComponent<Renderer> ().material.color = Color.red;
		} else {
			//ScoreText = GameObject.Find ("ScoreTextP2").GetComponent<Text> ();
			playerNumber = 2;
			GetComponent<Renderer> ().material.color = Color.green;
		}
		PlayerText.text = GameManagerScript.playerName;
		row = (int) transform.position.x / 6;
		col = (int) transform.position.z / 6;
		//ScoreText.text = GameManagerScript.playerName + " : 0";

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
				JumpAnimation (MoveRight,row,col);
				//stage.positionMatrix [row, col + 1] = playerNumber;
				UpdatePosition(row,col+1,playerNumber);
				//button = true;
			}
			if (((Input.GetAxisRaw ("Horizontal") == -1) || (leftButton == true)) && (ground == true) && (transform.position.z > 0) && (CheckFreeSpace (row, col - 1))) {
				//transform.position -= MoveRight;
				bug.text = Time.realtimeSinceStartup.ToString ();
				ground = false;
				JumpAnimation (-MoveRight,row,col);
				//stage.positionMatrix [row, col - 1] = playerNumber;
				UpdatePosition(row,col-1,playerNumber);
				//button = true;
			}
			if (((Input.GetAxisRaw ("Vertical") == -1) || (downButton == true)) && (ground == true) && (transform.position.x < 42) && (CheckFreeSpace (row + 1, col))) {
				//transform.position += MoveDown;
				ground = false;
				JumpAnimation (MoveDown,row,col);
				//stage.positionMatrix [row + 1, col] = playerNumber;
				UpdatePosition(row+1,col,playerNumber);
				//button = true;
			}
			if (((Input.GetAxisRaw ("Vertical") == 1) || (upButton == true)) && (ground == true) && (transform.position.x > 0) && (CheckFreeSpace (row - 1, col))) {
				//transform.position -= MoveDown;
				ground = false;
				JumpAnimation (-MoveDown,row,col);
				//stage.positionMatrix [row - 1, col] = playerNumber;
				UpdatePosition(row-1,col,playerNumber);
				//button = true;
			}
			if (Input.GetKeyDown (KeyCode.P)) {
				OpenPauseMenu ();
			}
		}
		/*if (ground == true) {
			//stage.stageMatrix[row,col] = playerNumber;
			UpdateStage(row,col,playerNumber);
			//stage.positionMatrix [row, col] = playerNumber;
			UpdatePosition(row,col,playerNumber);
			Change (stage.squares [(row * 8) + col].GetComponent<Renderer> (), playerNumber);
		}*/
	}

	void OnCollisionEnter (Collision other) {
		if (gameObject.GetComponent<PlayerScriptNetwork> ().enabled == false)
			return;
		//print ("player ID : " + isServer);
		if (other.transform.tag == "Square") {
			print("touch " + Time.realtimeSinceStartup);
			GameObject stagee = GameObject.FindGameObjectWithTag ("Stage");
			//stage.positionMatrix [row, col] = 0;
			UpdatePosition(row,col,0);
			row = (int) other.transform.position.x / 6;
			col = (int) other.transform.position.z / 6;
			if (pillPowerUp == true) {
				PillPower ();
			}
			//stage.stageMatrix[row,col] = playerNumber;
			transform.position = new Vector3 (row*6,0.75f,col*6);
			ground = true;
			print("ground " + Time.realtimeSinceStartup);
			//stage.positionMatrix [row, col] = playerNumber;
			//print("Collision" + playerNumber.ToString());
			UpdatePosition(row,col,playerNumber);
			UpdateStage(row,col,playerNumber,stagee);
			Change (row,col, playerNumber,stagee);
			//stage.CheckBorders (playerNumber);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (gameObject.GetComponent<PlayerScriptNetwork> ().enabled == false)
			return;
		if (other.gameObject.CompareTag ("Collectable")) {
			StageScriptNetwork stage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
			ScoreCalculation (other.transform.position);
			if (isServer) {
				NetworkServer.Destroy (other.gameObject);
				stage.CreateCollectable();
			} else {
				CmdDestroyObj (other.GetComponent<NetworkIdentity> ().netId);
				CmdRequestSpawnCollectable ();
			}
		}
		if (other.gameObject.CompareTag ("Arrow")) {
			print ("touched");
			StageScriptNetwork stage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
			float A = (other.transform.rotation.eulerAngles.y / 180) * Mathf.PI;
			ColorRow ((int) other.transform.position.z / 6, (int) other.transform.position.x / 6, (int)Mathf.Cos (A), (int)Mathf.Sin (A));
			if (isServer) {
				NetworkServer.Destroy (other.gameObject);
				stage.CreateArrow ();
			} else {
				CmdDestroyObj (other.GetComponent<NetworkIdentity> ().netId);
				stage.CmdRequestSpawnArrow ();
			}
		}
		if (other.gameObject.CompareTag ("Pill")) {
			pillPowerUp = true;
			transform.Find ("PillPower").gameObject.SetActive (true);
			StartCoroutine (PillTime ());
			if (isServer)
				NetworkServer.Destroy (other.gameObject);
			else
				CmdDestroyObj (other.GetComponent<NetworkIdentity> ().netId);
		}
	}
	//Spawn
	[Command]
	public void CmdRequestSpawnCollectable(){
		StageScriptNetwork stage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
		//print ("spawn now");
		stage.CreateCollectable ();
		//print ("spawn done");
	}

	//Destroy Command
	[Command]
	public void CmdDestroyObj(NetworkInstanceId ID){
		//print ("destruction");
		GameObject obj = NetworkServer.FindLocalObject (ID);
		NetworkServer.Destroy (obj);
	}
	//Update Position Functions
	[Command]
	public void CmdUpdatePosition(int Row,int Col,int PlayerNumber){
		RpcUpdatePosition (Row, Col, PlayerNumber);
	}

	public void UpdatePosition(int Row,int Col,int PlayerNumber){
		if (!isServer) {
			CmdUpdatePosition (Row, Col, PlayerNumber);
		} else {
			RpcUpdatePosition (Row, Col, PlayerNumber);
		}
	}

	[ClientRpc]
	public void RpcUpdatePosition(int row,int col,int Number)
	{
		StageScriptNetwork TheStage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
		TheStage.positionMatrix [row, col] = Number;
	}

	//Update Position Functions
	[Command]
	public void CmdUpdateStage(int Row,int Col,int PlayerNumber,GameObject thestage){
		StageScriptNetwork stage = thestage.GetComponent<StageScriptNetwork> ();
		stage.RpcUpdateStage (Row, Col, PlayerNumber);
	}

	public void UpdateStage(int Row,int Col,int PlayerNumber,GameObject thestage){
		if (!isServer) {
			CmdUpdateStage (Row, Col, PlayerNumber,thestage);
		} else {
			StageScriptNetwork stage = thestage.GetComponent<StageScriptNetwork> ();
			stage.RpcUpdateStage (Row, Col, PlayerNumber);
		}
	}

	//Update Score Functions
	[Command]
	public void CmdUpdateScore(int _player,int _sco,GameObject thestage,string _name){
		StageScriptNetwork stage = thestage.GetComponent<StageScriptNetwork> ();
		stage.RpcUpdateScore (_player,_sco,_name);
	}

	public void UpdateScore(int _player,int _sco,GameObject thestage,string _name){
		if (!isServer) {
			CmdUpdateScore (_player,_sco,thestage,_name);
		} else {
			StageScriptNetwork stage = thestage.GetComponent<StageScriptNetwork> ();
			stage.RpcUpdateScore (_player,_sco,_name);
		}
	}

	//Various Functions
	IEnumerator PillTime(){
		yield return new WaitForSeconds (5f);
		pillPowerUp = false;
		transform.Find ("PillPower").gameObject.SetActive (false);
	}
	/*[ClientRpc]
	void RpcChange(int _row,int _col,int playerNumber){
		Renderer rend = squares [(_row * 8) + _col].GetComponent<Renderer> ();
		print (rend.material.color);
		if (playerNumber == 0) {
			rend.material.color = Color.white;
		}
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
	}*/
	void Change(int _row,int _col,int _playerNumber,GameObject thestage){
		if (isServer) {
			//print ("server change");
			StageScriptNetwork stage = thestage.GetComponent<StageScriptNetwork> ();
			stage.RpcChange (_row, _col, _playerNumber);
		} else {
			//print ("client change");
			CmdChange (_row, _col, _playerNumber,thestage);
		}
	}

	[Command]
	void CmdChange(int _row,int _col,int _playerNumber,GameObject thestage){
		//print ("ha");
		StageScriptNetwork stage = thestage.GetComponent<StageScriptNetwork> ();
		stage.RpcChange (_row, _col, _playerNumber);
	}

	void PillPower(){
		StageScriptNetwork stage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
		if(row-1>=0){
			Change (row-1,col, playerNumber,stage.gameObject);
			//stage.stageMatrix[row-1,col] = playerNumber;
			UpdateStage(row-1,col,playerNumber,stage.gameObject);
		}
		if(row+1<8){
			Change (row+1,col, playerNumber,stage.gameObject);
			//stage.stageMatrix[row+1,col] = playerNumber;
			UpdateStage(row+1,col,playerNumber,stage.gameObject);
		}
		if(col-1>=0){
			Change (row,col-1, playerNumber,stage.gameObject);
			//stage.stageMatrix[row,col-1] = playerNumber;
			UpdateStage(row,col-1,playerNumber,stage.gameObject);
		}
		if(col+1<8){
			Change (row,col+1, playerNumber,stage.gameObject);
			//stage.stageMatrix[row,col+1] = playerNumber;
			UpdateStage(row,col+1,playerNumber,stage.gameObject);

		}
	}
	[Command]
	void CmdJumpAnimation(Vector3 JumpDirection,int _row,int _col) {
		print("command " + Time.realtimeSinceStartup);
		RpcJumpAnimation (JumpDirection,_row,_col);
	}

	[ClientRpc]
	void RpcJumpAnimation (Vector3 JumpDirection,int _row,int _col) {
		ActualJumpAnimation (JumpDirection, _row, _col);
	}

	void ActualJumpAnimation(Vector3 JumpDirection,int _row,int _col){
		print("started " + Time.realtimeSinceStartup);
		Animation Anim = gameObject.GetComponent<Animation> ();
		transform.position = new Vector3 (_row*6,0.8f,_col*6);
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
		print("finished " + Time.realtimeSinceStartup);
	}

	void JumpAnimation(Vector3 JumpDirection,int _row,int _col){
		if (isServer) {
			RpcJumpAnimation (JumpDirection,_row,_col);
		} else {
			CmdJumpAnimation (JumpDirection,_row,_col);
		}
	}

	void ScoreCalculation(Vector3 pos){
		//print ("entered score");
		StageScriptNetwork stage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
		int count = 0;
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (stage.stageMatrix [i, j] == playerNumber) {
					count++;
					Change (i, j, 0,stage.gameObject);
					//Renderer rend = stage.squares [(i * 8) + j].GetComponent<Renderer> ();
					//rend.material.color = Color.white;
					//stage.stageMatrix [i, j] = 0;
					UpdateStage(i,j,0,stage.gameObject);
				}
			}
		}
		//Score text script
		if (!isServer) {
			CmdShowScoreText (pos, count,playerNumber);
		} else {
			RpcShowScoreText (pos, count,playerNumber);
		}
		//end of score test script
		//stage.scores[playerNumber] += count;
		UpdateScore(playerNumber,stage.scores[playerNumber]+count,stage.gameObject,GameManagerScript.playerName);
		//ScoreText.text = GameManagerScript.playerName + " : " + stage.scores[playerNumber].ToString ();
	}
	[Command]
	void CmdShowScoreText(Vector3 pos,int score,int _playerNumber){
		RpcShowScoreText (pos,score,_playerNumber);
	}
	[ClientRpc]
	public void RpcShowScoreText(Vector3 pos,int score,int _playerNumber){
		GameObject clone = (GameObject)Instantiate (Scored, pos, Quaternion.Euler (0, -90, 0));
		Vector3 vect = clone.transform.position;
		vect.y = 4f;
		clone.transform.position = vect;
		TextMesh TheScore = clone.transform.Find ("Score").GetComponent<TextMesh> ();
		if (_playerNumber == 1) {
			TheScore.color = Color.red;
		}
		if (_playerNumber == 2) {
			TheScore.color = Color.green;
		}
		if (_playerNumber == 3) {
			TheScore.color = Color.yellow;
		}
		if (_playerNumber == 4) {
			TheScore.color = Color.blue;
		}
		TheScore.text = score.ToString ();
		Destroy (clone,3f);
	}

	void ColorRow (int Col,int Row,int xdirection,int ydirection) {
		StageScriptNetwork stage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
		int xactualPos = Col;
		int yactualPos = Row;
		while ((xactualPos >= 0) && (xactualPos < 8) && (yactualPos >= 0) && (yactualPos < 8)) {
			//stage.stageMatrix [yactualPos, xactualPos] = playerNumber;
			UpdateStage(yactualPos,xactualPos,playerNumber,stage.gameObject);
			Change (yactualPos,xactualPos, playerNumber,stage.gameObject);
			xactualPos += xdirection;
			yactualPos += ydirection;
		}
	}

	bool CheckFreeSpace(int Row,int Col){
		StageScriptNetwork stage = GameObject.FindGameObjectWithTag ("Stage").GetComponent<StageScriptNetwork> ();
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
