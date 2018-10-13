using System;
using UnityEngine;
using System.Collections;

public class StageScript : MonoBehaviour {
	[HideInInspector]
	public int[,] stageMatrix = new int[8,8];
	[HideInInspector]
	public int[,] positionMatrix = new int[8,8];
	[HideInInspector]
	public GameObject[] squares;
	[HideInInspector]
	public int[] scores = new int[6];

	public GameObject Collectable;
	public GameObject Arrow;
	public GameObject Pill;
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
				positionMatrix [i, j] = 0;
			}
		}
		for (int i = 0; i < 4; i++) {
			scores [i] = 0;
		}
		positionMatrix [0, 0] = 1;
		for (int i = 0; i < CollectableNumber; i++) {
			CreateCollecatble ();
		}
		for (int i = 0; i < ArrowNumber; i++) {
			CreateArrow ();
		}
		StartCoroutine (CreatePillRoutine());
	}

	void Update () {
		
	}

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

	public void CreateCollecatble(){
		int Row = (int)UnityEngine.Random.Range (0f, 7f);
		int Col = (int)UnityEngine.Random.Range (0f, 7f);
		while (positionMatrix [Row, Col] != 0) {
			Row = (int)UnityEngine.Random.Range (0f, 7f);
			Col = (int)UnityEngine.Random.Range (0f, 7f);
		}
		Instantiate (Collectable, new Vector3 (Row * 6, 1f, Col * 6), Quaternion.identity);
		positionMatrix [Row, Col] = 5;
		//Score Collectables are marked with the number 5 in the positionMatrix
	}
	public void CreateArrow(){
		int Row = (int)UnityEngine.Random.Range (0f, 7f);
		int Col = (int)UnityEngine.Random.Range (0f, 7f);
		while (positionMatrix [Row, Col] != 0) {
			Row = (int)UnityEngine.Random.Range (0f, 7f);
			Col = (int)UnityEngine.Random.Range (0f, 7f);
		}
		Instantiate (Arrow, new Vector3 (Row * 6, 1f, Col * 6), Quaternion.identity);
		positionMatrix [Row, Col] = 6;
		//Arrows are marked with the number 6 in the positionMatrix
	}

	public void CreatePill(){
		int Row = (int)UnityEngine.Random.Range (0f, 7f);
		int Col = (int)UnityEngine.Random.Range (0f, 7f);
		while (positionMatrix [Row, Col] != 0) {
			Row = (int)UnityEngine.Random.Range (0f, 7f);
			Col = (int)UnityEngine.Random.Range (0f, 7f);
		}
		Instantiate (Pill, new Vector3 (Row * 6, 1f, Col * 6), Quaternion.identity);
		positionMatrix [Row, Col] = 7;
		//Pills are marked with the number 7 in the positionMatrix
	}

	IEnumerator CreatePillRoutine(){
		yield return new WaitForSeconds (15f);
		while (true) {
			CreatePill ();
			yield return new WaitForSeconds (15f);
		}
	}
}
