using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : Interactable {

	public UIManager UIMan;

	public float bloodSugarValue;
	public float stabilizeDuration;
 

	public override void DisplayName () {
		UIMan.DisplayInteraction("Press E to pick up");
	}


	public override void Interact (Transform newParent) {
		transform.parent = newParent.transform;
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Collider>().isTrigger = true;
	}

	public override void Detach () {
		transform.parent = null;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Collider>().isTrigger = false;

	}
}
