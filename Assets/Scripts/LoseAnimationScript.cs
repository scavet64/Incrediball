using UnityEngine;
using System.Collections;

public class LoseAnimationScript : MonoBehaviour {

	//refrence for the pause menu panel in the hierarchy
	public GameObject losePanel;
	//animator reference
	private Animator anim;
	//variable for checking if the game is paused 
	private bool isGameOver {get; set;}

	// Use this for initialization
	void Start () {
		//unpause the game on start
		Time.timeScale = 1;
		//get the animator component
		anim = losePanel.GetComponent<Animator>();
		//disable it on start to stop it from playing the default animation
		anim.enabled = false;
		isGameOver = false;
	}

	public void animateEndScreen(){
		if (!anim.enabled) {
			anim.enabled = true;
			anim.Play ("ComeDown");
		}
	}

	public void bringMenu(){
		//if (!anim.enabled) {
			anim.enabled = true;
			anim.Play ("ComeDown");
		//}
	}

	public void removeMenu(){
		//if (!anim.enabled) {
			anim.enabled = true;
			anim.Play ("GoAway");
		//}
	}

	//function to pause the game
	public void PauseGame(){
		//enable the animator component
		anim.enabled = true;
		//play the Slidein animation
		anim.Play("PauseMenuSlideIn");
		//set the isPaused flag to true to indicate that the game is paused
		//isPaused = true;
		//freeze the timescale
		Time.timeScale = 0;
	}
	//function to unpause the game
	public void UnpauseGame(){
		//set the isPaused flag to false to indicate that the game is not paused
		//isPaused = false;
		//play the SlideOut animation
		anim.Play("PauseMenuSlideOut");
		//set back the time scale to normal time scale
		Time.timeScale = 1;
	}

}