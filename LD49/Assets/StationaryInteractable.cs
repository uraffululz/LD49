using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryInteractable : Interactable {

	[SerializeField] UIManager UIMan;


	public override void DisplayName () {
		UIMan.DisplayInteraction("Press E to interact");
	}


	public override void Interact (Transform target) {
		
	}


	public override void Detach () {
		
	}
}
