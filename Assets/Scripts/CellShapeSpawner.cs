using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CellShapeSpawner : MonoBehaviour {


	public GameObject[] cellShapes;
	public GameObject[] nextCellShapes;

	GameObject upNextObject = null;

	int shapeIndex = 0;
	int nextShapeIndex = 0;


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

		Instantiate (cellShapes [shapeIndex],
			transform.position,
			Quaternion.identity);

		nextShapeIndex = Random.Range (0, 8);

		Vector3 nextShapePos = new Vector3 (-11, 11, 0);

		if (upNextObject != null)
			Destroy (upNextObject);

		upNextObject = Instantiate (nextCellShapes [nextShapeIndex], 
			nextShapePos,
			Quaternion.identity);
	}

	void Start () {

		nextShapeIndex = Random.Range (0, 8);
		SpawnShape();

	}
	
}