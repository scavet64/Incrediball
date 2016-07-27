using UnityEngine;
using System.Collections;

public class SkinButtonScript : MonoBehaviour {

	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameController.control;
	}

	public void NextSkin(){
		gameController.setNextorPrevSkin (GameController.NEXTSKIN);
	}

	public void PrevSkin(){
		gameController.setNextorPrevSkin (GameController.PREVSKIN);
	}

}
