using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class StageScriptNetwork : NetworkBehaviour {
	[HideInInspector]
	public int[,] stageMatrix = new int[8,8];
	[HideInInspector]
	public int[,] positionMatrix = new int[8,8];
	[HideInInspector]
	public GameObject[] squares;
	[HideInInspector]
	public int[] scores = new int[6];
	public Text[] SText = new Text[4];

	public GameObject Collectable;
	public GameObject Arrow;
	public GameObject Pill;
	public Text bug;
	public int CollectableNumber = 3;
	public int ArrowNumber = 2;

	// Use this for initialization
	void Start () {
		//Sorting the squares by position in matrix
		squares = GameObject.FindGameObjectsWithTag ("Square");
		Array.Sort (squares, delegate(GameObject ob1, GameObject ob2) {
			return ((((int)ob1.transform.position.x / 6) * 10 + ((int)ob1.transform.position.z / 6)) - (((int)ob2.transform.position.x / 6) * 10 + ((int)ob2.transform.position.z / 6)));
		});
		for (int i=0; i<8; i++) {
			for(int j=0;j<8;j++)
			{
				stageMatrix[i,j] = 0;
				//positionMatrix [i, j] = 0;
			}
		}
		if (isServer) {
			for (int i = 0; i < 4; i++) {
				scores [i] = 0;
			}
			for (int i = 0; i < CollectableNumber; i++) {
				CreateCollectable ();
			}
			for (int i = 0; i < ArrowNumber; i++) {
				CreateArrow ();
			}
			StartCoroutine (CreatePillRoutine ());
		}
	}

	/*void Update () {
		//print (positionMatrix [3, 3]);
		bug.text = "";
		for (int i=0; i<8; i++) {
			for(int j=0;j<8;j++)
			{
				bug.text += positionMatrix [i, j].ToString()+" ";
			}
			bug.text += "\n";
		}
	}*/

	/*public override void OnStartServer(){
		
	}*/

	public void CheckBorders (int color) 
	{
		bool verif = true;
		for (int i = 0; i<8; i++) {
			if((stageMatrix[0,i] != color) || (stageMatrix[7,i] != color) || (stageMatrix[i,0] != color) || (stageMatrix[i,7] != color))
			{
				verif = false;
				break;
			}
		}
		if (verif == true) {
			for(int i=0;i<8;i++)
			{
				for (int j=0;j<8;j++)
				{
					stageMatrix[i,j] = color;
				}
			}
		}
	}
	[Command]
	public void CmdRequestSpawnArrow(){
		CreateArrow ();
	}

	public void CreateCollectable(){
		int Row = (int)UnityEngine.Random.Range (0f, 7f);
		int Col = (int)UnityEngine.Random.Range (0f, 7f);
		while (positionMatrix [Row, Col] != 0) {
			Row = (int)UnityEngine.Random.Range (0f, 7f);
			Col = (int)UnityEngine.Random.Range (0f, 7f);
		}
		GameObject collect = (GameObject)Instantiate (Collectable, new Vector3 (Row * 6, 1f, Col * 6), Quaternion.Euler(new Vector3(-90f,0,0)));
		positionMatrix [Row, Col] = 5;
		RpcUpdatePosition(Row,Col,5);
		NetworkServer.Spawn (collect);
		//Score Collectables are marked with the number 5 in the positionMatrix
	}
	public void CreateArrow(){
		int Row = (int)UnityEngine.Random.Range (0f, 7f);
		int Col = (int)UnityEngine.Random.Range (0f, 7f);
		while (positionMatrix [Row, Col] != 0) {
			Row = (int)UnityEngine.Random.Range (0f, 7f);
			Col = (int)UnityEngine.Random.Range (0f, 7f);
		}
		GameObject collect = (GameObject)Instantiate (Arrow, new Vector3 (Row * 6, 1f, Col * 6), Quaternion.identity);
		positionMatrix [Row, Col] = 6;
		RpcUpdatePosition(Row,Col,6);
		NetworkServer.Spawn (collect);
		//Arrows are marked with the number 6 in the positionMatrix
	}

	public void CreatePill(){
		int Row = (int)UnityEngine.Random.Range (0f, 7f);
		int Col = (int)UnityEngine.Random.Range (0f, 7f);
		while (positionMatrix [Row, Col] != 0) {
			Row = (int)UnityEngine.Random.Range (0f, 7f);
			Col = (int)UnityEngine.Random.Range (0f, 7f);
		}
		GameObject collect = (GameObject)Instantiate (Pill, new Vector3 (Row * 6, 1f, Col * 6), Quaternion.identity);
		positionMatrix [Row, Col] = 7;
		RpcUpdatePosition(Row,Col,7);
		NetworkServer.Spawn (collect);
		//Pills are marked with the number 7 in the positionMatrix
	}

	[ClientRpc]
	public void RpcUpdateStage(int _row,int _col,int _Number)
	{
		stageMatrix [_row, _col] = _Number;
	}

	[ClientRpc]
	public void RpcUpdateScore(int _player,int _sco,string _name)
	{
		scores [_player] = _sco;
		SText [_player - 1].text = _name + " : " + scores [_player];
	}

	[ClientRpc]
	public void RpcUpdatePosition(int _row,int _col,int _Number)
	{
		positionMatrix [_row, _col] = _Number;
	}

	[ClientRpc]
	public void RpcChange(int _row,int _col,int playerNumber){
		Renderer rend = squares [(_row * 8) + _col].GetComponent<Renderer> ();
		//print ("changing " + playerNumber.ToString ());
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
	}

	IEnumerator CreatePillRoutine(){
		yield return new WaitForSeconds (15f);
		while (true) {
			CreatePill ();
			yield return new WaitForSeconds (15f);
		}
	}
}
