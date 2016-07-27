using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	private static MusicController instance = null;
	
	void Awake(){
		Debug.Log("music player awake " + this.GetInstanceID());
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
