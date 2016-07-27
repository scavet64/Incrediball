using UnityEngine;
using System.Collections;

public class SkinManager : MonoBehaviour {

	public Material[] materialArray;

	public Material getMaterial(int index){
		return materialArray [index];
	}


}
