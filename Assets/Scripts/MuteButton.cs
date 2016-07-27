using UnityEngine;
using System.Collections;

public class MuteButton : MonoBehaviour {

	public GameObject muteButton;
	public GameObject unMuteButton;

	// Use this for initialization
	void Start () {
		if (GameController.control.isMuted) {
			muteButton.SetActive (false);
			unMuteButton.SetActive (true);
		} else {
			muteButton.SetActive (true);
			unMuteButton.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleMute(){
		GameController.control.toggleMute ();
		muteButton.SetActive (!muteButton.activeSelf);
		unMuteButton.SetActive (!muteButton.activeSelf);
	}
}
