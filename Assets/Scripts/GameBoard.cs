using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

	public static Transform[,] gameBoard = new Transform[15, 30];

	public static bool DeleteAllFullRows() {
		// DeleteAllFullRows is meant to delete a row if every cell
		// is full. Award the player for each symbol combined.

		for (int row = 0; row < 30; ++row) {

			if (IsRowFull (row)) {

				DeleteGBRow (row);

				// Temp sound, need to find better SFX.
				SoundManager.Instance.PlayOneShot (SoundManager.Instance.rowDelete);

				return true;
			}
				
		}
		return false;

	}

	public static bool IsBoardFailure() {
		
		for (int col = 0; col < 15; ++col) {

			if (gameBoard [col, 27] != null) {
				return true;
			}
		}
		return false;
	}

	public static bool IsRowFull(int row) {
		// IsRowFull is meant to return true if a row is full of cells.

		for (int col = 0; col < 15; ++col) {

			if (gameBoard [col, row] == null) {
				return false;
			}
		}
		return true;
	}

	public static void DeleteGBRow(int row) {
		// DeleteGBRow removes all cells in a row on the gameboard.

		for (int col = 0; col < 15; ++col) {

			Destroy (gameBoard [col, row].gameObject);

			gameBoard [col, row] = null;
		}

		row++;

		for (int j = row; j < 30; ++j) {

			for (int col = 0; col < 15; ++col) {

				if (gameBoard [col, j] != null) {

					gameBoard [col, j - 1] = gameBoard [col, j];
					gameBoard [col, j] = null;
					gameBoard[col, j-1].position += new Vector3(0, -1, 0);

				}
			}
		}
	}

	public static void DeleteAllRows() {
		// DeleteAllRows removes all cells on the gameboard.

		for (int row = 0; row < 30; ++row) {

			for (int col = 0; col < 15; ++col) {

				if (gameBoard [col,row] != null)
					Destroy (gameBoard [col, row].gameObject.transform.parent.gameObject);
				gameBoard [col, row] = null;

			}
		}
	}

}