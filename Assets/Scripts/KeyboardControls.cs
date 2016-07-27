using UnityEngine;
using System.Collections;

public class KeyboardControls : MonoBehaviour {
	
	private PlayerController playerController;

	// Use this for initialization
	void Start () {
		playerController = this.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.LeftArrow)) {
			playerController.isMovingLeft = true;
		} else {
			playerController.isMovingLeft = false;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			playerController.isMovingRight = true;
		} else {
			playerController.isMovingRight = false;
		}

	}
}
