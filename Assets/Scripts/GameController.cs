using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using UnityEditor.Advertisements;

public class GameController : MonoBehaviour {

	//public variables set by unity editor
	public static GameController control;
	public Material[] skins;
	public AdvertManager adManager;

	public int timesPlayedToday { get; set; }
	public const int pickupsForEachSkin = 100;


	//public variables used for skins
	public static int NEXTSKIN = 1;
	public static int PREVSKIN = -1;

	//Player Information
	private List<int> ownedSkinsByIndex;
	public float bestDistance { get; set; }				//players best distance traveled
	public int numberOfPickups { get; set; }			//number of pickups player has collected
	public int currentSkinIndex { get; set; }			//currentSkinIndex the player has on
	public int tillNextSkin { get; set; }				//number of coconuts a player needs
	public bool isMuted { get; set; }					//if the player has the game muted or not
	public int lastUsedQualitySetting { get; set; }		//best quality setting that was determined on initial launch

	private AudioSource audioSource;
	private AdvertManager advertmanager;
	private BallColorScript colorScript;
	private int skinCounter;
	private GameObject ball;
	public bool isNewSkinUnlocked;


	// Use this for initialization
	void Awake() {
		if (control != null) {
			Destroy (gameObject);
		} else {
			load ();														//load the players old information into datafields
			control = this;													//put this object into the static control field
			audioSource = this.GetComponent<AudioSource> ();				//get the AudioSource component from the gameobject
			GameObject.DontDestroyOnLoad (gameObject);						//ensure this is the only instance of this game object
			adManager = AdvertManager.adManager;							//store reference to singleton admanager
			audioSource.mute = isMuted;										//set the audiosource to its correct muted state
			timesPlayedToday = 0;											//initialize the number of plays this session to 0
			//Debug.Log (Application.persistentDataPath);
			QualitySettings.SetQualityLevel(lastUsedQualitySetting, true);	//set the quality setting using last quality setting
			//unlockAllSkins ();
			Application.targetFrameRate = 60;
		}
	}

	public void unlockSkin(){
		if(!allSkinsUnlocked()){
			//Debug.Log("Unlocking Skin");
			//Debug.Log("ArraySize = " + ownedSkinsByIndex.Count);
			ownedSkinsByIndex.Add (ownedSkinsByIndex.Count);
		} else {
			//Debug.Log ("all skins unlocked");
		}
	}

	void unlockAllSkins(){
		while (!allSkinsUnlocked ()) {
			unlockSkin ();
		}
	}

	public bool allSkinsUnlocked(){
		bool isSkinsUnlocked = false;

		if (ownedSkinsByIndex.Count >= skins.Length) {
			isSkinsUnlocked = true;
		}

		return isSkinsUnlocked;
	}

	public void toggleMute(){
		audioSource.mute = !audioSource.mute;
		isMuted = !isMuted;
	}

	public void setNextorPrevSkin(int nextSkin){
		if (colorScript == null || ball == null) {
			getNewBallAndColorScript ();
		}
		if (ownedSkinsByIndex.Count != 0) {
			int nextSkinIndex = (currentSkinIndex + ownedSkinsByIndex.Count + nextSkin) % ownedSkinsByIndex.Count;
			colorScript.changeSkin (skins [ownedSkinsByIndex [nextSkinIndex]]);
			currentSkinIndex = nextSkinIndex;
		}
		save ();
	}

	void getNewBallAndColorScript(){
		ball = GameObject.FindGameObjectWithTag ("Ball");
		colorScript = ball.GetComponent<BallColorScript> ();
	}

	public void updatePickup(int numberOfPickups){
		this.numberOfPickups += numberOfPickups;
		if (!allSkinsUnlocked ()) {
			this.tillNextSkin -= numberOfPickups;
			if (tillNextSkin <= 0) {
				unlockSkin ();
				isNewSkinUnlocked = true;
				tillNextSkin = pickupsForEachSkin + tillNextSkin;
			}
		}
	}

	public void save(){
		BinaryFormatter bf = new BinaryFormatter ();
		Debug.Log (Application.persistentDataPath);
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.bestDistance = bestDistance;
		data.numberOfPickups = numberOfPickups;
		data.ownedSkinsByIndex = ownedSkinsByIndex;
		data.currentSkinIndex = currentSkinIndex;
		data.tillNextSkin = tillNextSkin;
		data.isMuted = isMuted;
		data.lastUsedQualitySetting = QualitySettings.GetQualityLevel();

		bf.Serialize (file, data);
		file.Close ();
	}

	void load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = bf.Deserialize (file) as PlayerData;
			file.Close ();

			bestDistance = data.bestDistance;
			numberOfPickups = data.numberOfPickups;
			ownedSkinsByIndex = data.ownedSkinsByIndex;
			currentSkinIndex = data.currentSkinIndex;
			tillNextSkin = data.tillNextSkin;
			isMuted = data.isMuted;
			lastUsedQualitySetting = data.lastUsedQualitySetting;
		} else {
			bestDistance = 0f;
			numberOfPickups = 0;
			ownedSkinsByIndex = new List<int>();
			currentSkinIndex = 0;
			tillNextSkin = pickupsForEachSkin;
			isMuted = false;
			lastUsedQualitySetting = 0;

			//adds the initial skin as owned
			ownedSkinsByIndex.Add (0);
		}
	}

	[Serializable]
	class PlayerData{
		public float bestDistance;
		public int numberOfPickups;
		public List<int> ownedSkinsByIndex;
		public int currentSkinIndex;
		public int tillNextSkin;
		public bool isMuted;
		public int lastUsedQualitySetting;
	}
}
