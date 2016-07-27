using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	private int numberOfPickups;
	private ArrayList ownedMaterials;

	void Start(){
		ownedMaterials = new ArrayList ();
	}

	public bool isOwnedMaterial(Material material){
		return ownedMaterials.Contains (material);
	}

	public bool canPurchase(int cost, Material material){
		bool canPurchase = false;
		if (!isOwnedMaterial (material) && cost < numberOfPickups) {
			canPurchase = true;
		}
		return canPurchase;
	}

	public void purchaseMaterial(int cost, Material material){
		numberOfPickups -= cost;
		ownedMaterials.Add (material);
	}

	private void saveData(){
		PlayerPrefs.SetInt ("Pickups", numberOfPickups);
	}
}
