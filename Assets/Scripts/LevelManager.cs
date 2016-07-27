using UnityEngine;
using System.Collections;
//using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Advertisement.Initialize ("1073423", true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel(string name){
		Debug.Log ("Level Load requested for " + name);
		SceneManager.LoadScene (name);
	}


	public void QuitRequest(){
		Debug.Log ("I want to quit");
		Application.Quit ();
	}

	public void loadNextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
