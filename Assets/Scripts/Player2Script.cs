using UnityEngine;
using System.Collections;

public class Player2Script : MonoBehaviour {
	private int row;
	private int col;
	public int playerNumber;
	public StageScript stage;
	private bool button;
	private Vector3 MoveRight;
	private Vector3 MoveDown;

	// Use this for initialization
	void Start () {
		MoveRight.Set(0,0,6);
		MoveDown.Set (6, 0, 0);
		row = (int) transform.position.x / 6;
		col = (int) transform.position.z / 6;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Input.GetAxisRaw ("Horizontal2") == 0) && (Input.GetAxisRaw ("Vertical2") == 0)) {
			button = false;
		}

		if ((Input.GetAxisRaw ("Horizontal2") == 1) && (button == false) && (transform.position.z < 42)) {
			transform.position += MoveRight;
			button = true;
		}
		if ((Input.GetAxisRaw ("Horizontal2") == -1) && (button == false) && (transform.position.z > 0)) {
			transform.position -= MoveRight;
			button = true;
		}
		if ((Input.GetAxisRaw ("Vertical2") == -1) && (button == false) && (transform.position.x < 42)) {
			transform.position += MoveDown;
			button = true;
		}
		if ((Input.GetAxisRaw ("Vertical2") == +1) && (button == false) && (transform.position.x > 0)) {
			transform.position -= MoveDown;
			button = true;
		}
		stage.stageMatrix[row,col] = playerNumber;
	}

	void OnCollisionEnter (Collision other) {
		if (other.transform.tag == "Square") {
			stage.positionMatrix [row, col] = 0;
			row = (int) other.transform.position.x / 6;
			col = (int) other.transform.position.z / 6;
			stage.stageMatrix[row,col] = playerNumber;
			stage.positionMatrix [row, col] = playerNumber;
		}
		stage.CheckBorders (playerNumber);
	}
}
