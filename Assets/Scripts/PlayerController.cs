using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;	//adjustable speed set in editor initially. Could be made private instead TODO
	public AudioClip[] audioArray;	//audio array containing the sound effects for the player - Set in editor.

	//TESTING ERROR ON ANDROID

	//in game overlay - Set in editor
	public GameObject inGameOverlay;
	public Text countText;
	public Text distanceText;
//	public GameObject controlsBillboard;

	//end screen score text - Set in editor
	public GameObject losePanel;
	public Text best;
	public Text totalPickups;
	public Text finalDistance;
	public GameObject newSkinPanel;

	//interact with buttons
	public bool isMovingLeft  {get; set;}
	public bool isMovingRight {get; set;}

	//audio constants corrisponding to editor values
	private const int PICKUPSOUND = 0;
	private const int WALLHITSOUND = 1;
	//private const int ROLLINGSOUND = 2; //not used yet

	//determined forces for moving left and right
	//private Vector3 leftForce;
	//private Vector3 rightForce;

	private bool isGameOver;
	private float playerZStartOffset;
	private float zDistanceTraveled;
	private Rigidbody rb;
	private int currentPickupCount;
	private GameController controller;
	private AudioSource audioSource;


	//---------------------------------------------------------------------------------------------------------//

	public void moveLeft(){
		//Debug.Log ("Left");
		//rb.AddForce (leftForce);
		rb.AddForce (new Vector3 (-2*speed, 0 * speed, 0*speed));
	}

	public void moveRight(){
		//Debug.Log ("Right");
		//rb.AddForce (rightForce);
		rb.AddForce (new Vector3 (2*speed, 0 * speed, 0*speed));
	}

	void Start () {

		//initialize our variables
		isMovingLeft = false;
		isMovingRight = false;
		isGameOver = false;
		playerZStartOffset = this.transform.position.z;
		//losePanel.transform.Translate (new Vector3 (0, -1000, 0));
		//rightForce = new Vector3 (2*speed, 0 * speed, 0*speed);
		//leftForce = new Vector3 (-2*speed, 0 * speed, 0*speed);
		controller = GameController.control;
		audioSource = gameObject.AddComponent<AudioSource> ();

		//get components from gameObject
		rb = GetComponent<Rigidbody>();

		//set the constant movement for the ball
		Vector3 movement = new Vector3 (0*speed, -1 * speed, 2*speed);
		rb.velocity = movement;

		//set the text elements
		currentPickupCount = 0;
		updateCount ();


		if (GameController.control.timesPlayedToday > 0){
			if (GameController.control.timesPlayedToday % 3 == 0) {
				//time for a new banner
				requestBanner();
			}
		} else {
			//first time playing today
			this.transform.position += new Vector3(0,0,-10);
			requestBanner ();
//			controlsBillboard.SetActive (true);
		}
		GameController.control.timesPlayedToday++;
	}

	void requestBanner(){
		try{
			AdvertManager.adManager.RequestBanner ();
		} catch (Exception e) {
			Debug.Log (e.StackTrace);
		}
	}

	void FixedUpdate(){
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

		//Vector3 movement = new Vector3 (0*speed, -1 * speed, 1*speed);

		//rb.AddForce (movement);
		//rb.velocity = movement;

		if (!isGameOver) {
			increaseSpeedPerTime ();
			if (isMovingLeft) {
				moveLeft ();
			}
			if (isMovingRight) {
				moveRight ();
			}

			if (rb.velocity.z < speed) {
				//ensures velocity is at speed
				rb.velocity += new Vector3 (0f, 0f, speed - rb.velocity.z);
			}
		} else {
			rb.velocity = Vector3.zero;
			transform.Rotate (new Vector3 (0f, 30f, 0f) * Time.deltaTime);
		}

		//cleanUpAudio ();
		updateDistanceTraveled ();
		updateDistanceLabel ();
	}

	/**
	 * Updates the distance traveled value
	 **/
	void updateDistanceTraveled(){
		zDistanceTraveled = this.transform.position.z - playerZStartOffset;
	}

	/**
	 * Updates the distance traveled label 
	 **/
	void updateDistanceLabel (){
		if (zDistanceTraveled < 0) {
			distanceText.text = "Distance: 0";
		} else {
			distanceText.text = "Distance: " + Mathf.RoundToInt (zDistanceTraveled).ToString ();
		}
	}

	void increaseSpeedPerDistance (){
		speed = this.transform.position.z/50;
		if (speed < 3)
			speed = 3;
	}

	void increaseSpeedPerTime(){
		//speed += Time.deltaTime;
		//speed += .05f*Time.deltaTime;
		speed += .1f*Time.deltaTime;
	}

	/**
	 * Cleans up any audio sources that arent playing
	 **/
	void cleanUpAudio(){
		Component[] comps = GetComponents<AudioSource>();
		foreach (Component comp in comps) {
			AudioSource taudio = comp as AudioSource;
			if (!taudio.isPlaying) {
				DestroyObject (taudio);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive (false);
			currentPickupCount++;
			updateCount();

			playPassedInAudio (0);
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("Wall")) {
			playPassedInAudio (1);
		}
		if (other.gameObject.CompareTag ("Lost")) {
			collectAndStoreData ();
			showEndScreen ();
			rb.velocity = Vector3.zero;
			isGameOver = true;
		}
	}

	//check to see if this score is better than previous
	//store data locally
	void collectAndStoreData (){
		if (controller.bestDistance < zDistanceTraveled) {
			//new Best!
			controller.bestDistance = zDistanceTraveled;
			//PlayerPrefs.SetFloat("Best",zDistanceTraveled);
		}

		//pickups
		controller.updatePickup(currentPickupCount);
		//PlayerPrefs.SetInt("Pickups", currentPickupCount + PlayerPrefs.GetInt("Pickups"));
		controller.save ();
	}

	void showEndScreen(){
		losePanel.SetActive (true);
		int roundedDistance = Mathf.RoundToInt (zDistanceTraveled);

		//hide ingame GUI
		inGameOverlay.SetActive (false);

		finalDistance.text = roundedDistance.ToString ();
		best.text = Mathf.RoundToInt (controller.bestDistance).ToString ();

		totalPickups.text = controller.numberOfPickups.ToString ();
		LoseAnimationScript loseScript = losePanel.GetComponent<LoseAnimationScript> ();
		loseScript.animateEndScreen ();

		if (controller.isNewSkinUnlocked) {
			newSkinPanel.SetActive (true);
			controller.isNewSkinUnlocked = false;
		}
		Debug.Log (GameController.control.timesPlayedToday);
		if (GameController.control.timesPlayedToday % 5 == 0){
			Debug.Log ("Showing Interstitial");
			Debug.Log (GameController.control.timesPlayedToday);
			AdvertManager.adManager.showInterstitial ();
		} else if(GameController.control.timesPlayedToday % 5 == 1){
			AdvertManager.adManager.RequestInterstitial();
		}

	}

	void playPassedInAudio(int index){
		if (!GameController.control.isMuted) {
			audioSource.clip = audioArray [index];
			audioSource.volume = 0.25f;
			audioSource.Play ();
		}
	}

	private void updateCount(){
		countText.text = "Coconuts: " + currentPickupCount.ToString();
		//Debug.Log (currentPickupCount);
		//Debug.Log (CubeSpawner.numberOfPickups);
//		if (currentPickupCount >= CubeSpawner.numberOfPickups) {
//			winText.gameObject.SetActive (true);
//			winText.text = "You Win!";
//		}
	}
}
