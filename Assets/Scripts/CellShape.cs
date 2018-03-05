using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CellShape : MonoBehaviour {

	public static float fallSpeed = 1;

	// Tracks the last time the shape moved down.
	float lastMoveDown = 0;

	void Start () {

		if (!IsInGrid ()) {

			// Temp sound for board failure, need better SFX.
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.gameOver);

			Invoke("BoardFailure", .5f);
		}

	}
		
	void BoardFailure () {
		Destroy(gameObject);
		// TODO: erase board and deal dmg/remove all ap.
	}

	void IncreaseSpeed() { // TODO: actually have enemy use this.
		CellShape.fallSpeed -= .001f;
	}

	void Update() {

			// -------------- Move Left --------------
			if (Input.GetKeyDown ("a")) {
				transform.position += new Vector3 (-1, 0, 0);
				if (!IsInGrid ()) {
					transform.position += new Vector3 (1, 0, 0);
				} else {
					UpdateGameBoard ();
					// Temp sound for cell shape movement, need better SFX.
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.shapeMove);
				}

			}
			// -------------- Move Right --------------
			if (Input.GetKeyDown ("d")) {
				transform.position += new Vector3 (1, 0, 0);
				if (!IsInGrid ()) {
					transform.position += new Vector3 (-1, 0, 0);
				} else {
					UpdateGameBoard ();
					// Temp sound for cell shape movement, need better SFX.
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.shapeMove);
				}
			}

			// -------------- Drop Down --------------
			if (Input.GetKey ("s") || Time.time - lastMoveDown >= CellShape.fallSpeed) {
				transform.position += new Vector3 (0, -1, 0);
				if (!IsInGrid ()) {
					transform.position += new Vector3 (0, 1, 0);

					bool rowDeleted = GameBoard.DeleteAllFullRows ();

					if (rowDeleted) {
						GameBoard.DeleteAllFullRows ();
						increaseTextUIScore ();
					}

					// disable movement when no longer able to drop.
					enabled = false;

					// Bring in next cell shape.
					FindObjectOfType<CellShapeSpawner> ().SpawnShape ();

					// Play the sound
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.shapeStop);

				} else {
					UpdateGameBoard ();

					// Temp sound for cell shape movement, need better SFX.
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.shapeMove);
				}

				lastMoveDown = Time.time;

			}

			// -------------- Rotations --------------
			if (Input.GetKeyDown ("e")) {
				transform.Rotate (0, 0, -90);

				if (!IsInGrid ()) {
					transform.Rotate (0, 0, 90);
				} else {
					UpdateGameBoard ();

					// Temp sound for cell shape rotation, need better SFX.
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.rotateSound);
				}
			}
			if (Input.GetKeyDown ("w")) {
				transform.Rotate (0, 0, 90);

				if (!IsInGrid ()) {
					transform.Rotate (0, 0, -90);
				} else {
					UpdateGameBoard ();

					// Temp sound for cell shape rotation, need better SFX.
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.rotateSound);
				}
			}
	}

	public bool IsInGrid() {
		foreach (Transform childBlock in transform) {

			Vector2 vect = RoundVector(childBlock.position);

			if (!IsInBorder(vect)) {
				return false;
			}
	
			if (GameBoard.gameBoard [(int)vect.x, (int)vect.y] != null &&
				GameBoard.gameBoard [(int)vect.x, (int)vect.y].parent != transform) 
			{
				return false;
			}

		}
		return true;
	}

	public Vector2 RoundVector(Vector2 vect) {
		return new Vector2 (Mathf.Round (vect.x), 
			Mathf.Round (vect.y));
	}

	public static bool IsInBorder(Vector2 pos) {
		return ((int)pos.x >= 0 &&
			(int)pos.x <= 14 &&
			(int)pos.y >= 0);
	}

	public void UpdateGameBoard() {

		for (int y = 0; y < 30; ++y) {

			for (int x = 0; x < 15; ++x) {

				if (GameBoard.gameBoard [x, y] != null &&
					GameBoard.gameBoard[x, y].parent == transform) {
					GameBoard.gameBoard[x, y] = null;
				}
			}

		}

		foreach (Transform childBlock in transform) {

			Vector2 vect = RoundVector(childBlock.position);
			GameBoard.gameBoard [(int)vect.x, (int)vect.y] = childBlock;
		}

	}

	void increaseTextUIScore() { 
		// TODO: changed how this system works to
		// allow for different cell types.

		var textUIComp = GameObject.Find("AP")
			.GetComponent<Text>();
		int score = int.Parse(textUIComp.text);
		score++;
		textUIComp.text = score.ToString();
	}

}