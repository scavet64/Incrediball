using UnityEngine;
using System.Collections;

public class BallColorScript : MonoBehaviour {

	private MeshRenderer meshRenderer;
	private GameController controller;

	void Awake(){
		this.meshRenderer = this.GetComponent<MeshRenderer> ();
		controller = GameController.control;
		meshRenderer.material = controller.skins [controller.currentSkinIndex];
	}
		
	public void changeSkin(Material skin){
		this.meshRenderer = this.GetComponent<MeshRenderer> ();
		this.meshRenderer.material = skin;
	}

}
