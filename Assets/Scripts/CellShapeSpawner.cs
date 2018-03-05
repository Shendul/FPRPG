using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellShapeSpawner : MonoBehaviour {


	public GameObject[] cellShapes;
	public GameObject[] nextCellShapes;

	GameObject upNextObject = null;

	int shapeIndex = 0;
	int nextShapeIndex = 0;

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