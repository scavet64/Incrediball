using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	//private float speedMod = 10.0f;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
		if (Input.GetKey (KeyCode.J)) {
			Vector3 point = player.transform.position;
			Debug.Log(point);
			Debug.Log(transform.position);
		}
	}
}
