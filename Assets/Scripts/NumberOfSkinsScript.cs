using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberOfSkinsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string textString;
		int tillNextSkin = GameController.control.tillNextSkin;
		string properGrammerCoconut;

		if (tillNextSkin == 1) {
			properGrammerCoconut = "coconut";
		} else {
			properGrammerCoconut = "coconuts";
		}

		if (!GameController.control.allSkinsUnlocked ()) {
			textString = "Collect " + tillNextSkin + " more " + properGrammerCoconut + " to unlock a new skin!";
		} else {
			textString = "You have unlocked all of the skins!";
		}

		this.GetComponent<Text> ().text = textString;
	}
}
