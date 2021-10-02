using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	[SerializeField] UIManager UIMan;

	GameObject mainCam;
 
	bool canInteract = true;
	//public bool interacting;
	public GameObject currentInteractable;

	RaycastHit interactHit;
	[SerializeField] GameObject hitObject;


    void Start() {
        mainCam = Camera.main.gameObject;
    }

   

    void Update() {
       // bool canInteract = false;
		//RaycastHit interactHit;

		if (currentInteractable == null && Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out interactHit, 2f, 1 << 6)) {
			hitObject = interactHit.collider.gameObject;

			//if (hitObject != null) {
			if (hitObject.GetComponent<PickupInteractable>() != null) {
				hitObject.GetComponent<PickupInteractable>().DisplayName();
				canInteract = true;
			}
			else if (hitObject.GetComponent<StationaryInteractable>() != null) {
				hitObject.GetComponent<StationaryInteractable>().DisplayName();
				canInteract = true;
			}
			else {
				canInteract = false;
				UIMan.ClearInteraction();
			}
			//}
		}
		else if (currentInteractable != null && currentInteractable.GetComponent<PickupInteractable>()) {
			UIMan.DisplayInteraction("Press E to drop");
		}
		else {
			hitObject = null;
			UIMan.ClearInteraction();
		}
    }


	void OnInteract() {
		if (currentInteractable != null) {
			if (currentInteractable.GetComponent<PickupInteractable>()) {
				currentInteractable.GetComponent<PickupInteractable>().Detach();
			}
			
			currentInteractable = null;
		}
		else if (hitObject != null && canInteract) {
			currentInteractable = hitObject;

			if (currentInteractable.GetComponent<PickupInteractable>()) {
				currentInteractable.GetComponent<PickupInteractable>().Interact(mainCam.transform);
			}
			else if (currentInteractable.GetComponent<StationaryInteractable>()) {
				currentInteractable.GetComponent<StationaryInteractable>().Interact(mainCam.transform);
			}
			
		}
	}
}
