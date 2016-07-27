using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {
		 
	public GameObject pickupPreFab;
	public GameObject floorPrefab;

	private GameObject cubeFloor;
	private GameObject pickUps;

	// Use this for initialization
	void Start () {
		pickUps = new GameObject ();
		cubeFloor = new GameObject ();
		//createCubes ();
		createSandFloor();
	}

	void createSandFloor(){

		int finalZ = 1000;
		int previousX = 0;
		int randomX;
		for (int i = 0; i < finalZ; i += 2) {
			randomX = Random.Range (previousX - 2, previousX + 3);

			Vector3 pos = new Vector3 (randomX, -0.0f, i + 11);
			//Vector3 pos = new Vector3 (randomX, -0.25f, i + 11);
			GameObject sandFloor = Instantiate (floorPrefab, pos, Quaternion.identity) as GameObject;
			sandFloor.transform.SetParent (cubeFloor.transform);

			previousX = randomX;

			if (Random.value < 0.2) {
				createPickup (pos);
			}
		}
	}

	void createPickup(Vector3 pickupSpawnLocation){
		Vector3 pos = pickupSpawnLocation + new Vector3(0,1f,0);
		GameObject pickupObject = Instantiate(pickupPreFab, pos, Quaternion.identity) as GameObject;
		pickupObject.transform.SetParent (pickUps.transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createCubes(){

		int finalZ = 3000;
		int previousX = 0;
		int randomX;
		for (int i = 0; i < finalZ; i += 2) {
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.SetParent (cubeFloor.transform);
			randomX = Random.Range (previousX - 2, previousX + 3);
			//randomX = Random.Range (previousX - 1, previousX + 1);
			previousX = randomX;

			cube.transform.localScale = new Vector3(2f,1f,2f);
			//cube.transform.localScale.z = 2;
			cube.transform.position = new Vector3(randomX, 0f, i + 11);

			if (Random.value < 0.2) {
				createPickup (cube.transform.position);
			}
		}
	}
}
