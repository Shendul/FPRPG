using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CellShapeSpawner : MonoBehaviour {

	// BUG LIST: Gameboard breaks when orange shape is up next.


	public GameObject[] cellShapes;
	public GameObject[] nextCellShapes;
	public Sprite[] cellSprites;

	GameObject upNextObject = null;
	GameObject currentShape = null;

	int shapeIndex = 0;
	int nextShapeIndex = 0;
	int symbol1 = 0;
	int symbol2 = 0;
	int symbol3 = 0;
	int symbol4 = 0;


	public bool IsBoardFailure() {
		if (GameBoard.IsBoardFailure()) {
			// Temp sound for board failure, need better SFX.
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.gameOver);

			StartCoroutine(BoardFailure());
			return true;
		} else {
			return false;
		}
	}

	IEnumerator BoardFailure () {
		// TODO: deal dmg/remove all ap.
		GameBoard.DeleteAllRows(); // reset board

		//TODO: add some kind of cool blood splatter animation type thing.

		// Take away half of the heroes health for board failure.
		var HPText = GameObject.Find("HP")
			.GetComponent<Text>();
		int health = int.Parse(HPText.text);
		health -= Hero.maxHP / 2;
		HPText.text = health.ToString();

		if (health <= 0) {
			// TODO: add gameover scene.
			yield break;
		}

		print(Time.time); // for debug purposes
        yield return new WaitForSeconds(3);
        print(Time.time); // for debug purposes
		SpawnShape();
	}

	public void SpawnShape(){

		shapeIndex = nextShapeIndex;

		currentShape = Instantiate (cellShapes [shapeIndex],
			transform.position,
			Quaternion.identity);

		currentShape.transform.GetChild (0).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol1];
		currentShape.transform.GetChild (1).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol2];
		currentShape.transform.GetChild (2).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol3];
		currentShape.transform.GetChild (3).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol4];

		nextShapeIndex = Random.Range (0, 8);

		Vector3 nextShapePos = new Vector3 (-11, 11, 0);

		if (upNextObject != null)
			Destroy (upNextObject);

		upNextObject = Instantiate (nextCellShapes [nextShapeIndex], 
			nextShapePos,
			Quaternion.identity);

		symbol1 = Random.Range (0, 2);
		symbol2 = Random.Range (0, 2);
		symbol3 = Random.Range (0, 2);
		symbol4 = Random.Range (0, 2);


		upNextObject.transform.GetChild (0).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol1];
		upNextObject.transform.GetChild (1).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol2];
		upNextObject.transform.GetChild (2).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol3];
		upNextObject.transform.GetChild (3).gameObject.
			GetComponent<SpriteRenderer>().sprite = this.cellSprites[symbol4];
	}

	void Start () {

		nextShapeIndex = Random.Range (0, 8);
		SpawnShape();

	}
	
}