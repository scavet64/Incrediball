using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fadeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log (GameController.control.timesPlayedToday);
		if (GameController.control.timesPlayedToday <= 1) {
			Image imgComp = this.GetComponent<Image> ();
			imgComp.color = new Color(imgComp.color.r, imgComp.color.b, imgComp.color.g, 125);
			imgComp.CrossFadeAlpha (0, 3, false);
		}
	}
}
