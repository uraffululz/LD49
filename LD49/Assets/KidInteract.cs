using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidInteract : MonoBehaviour {

	[SerializeField] GameObject player;

	KidAggroManager aggroMan;
	KidMove moveScript;
 
	[SerializeField] GameObject handSlot;

	[SerializeField] bool interacting;


    void Awake() {
		aggroMan = GetComponent<KidAggroManager>();
        moveScript = GetComponent<KidMove>();
    }

   

    void Update() {
        
    }


	void OnTriggerEnter (Collider other) {
		if (!interacting && other.gameObject == player.GetComponent<PlayerInteraction>().currentInteractable && other.GetComponent<PickupInteractable>()) {
			player.GetComponent<PlayerInteraction>().currentInteractable = null;
			interacting = true;

			if (other.CompareTag("Food")) {
				GiveObjectToKid(other.gameObject);
				StartCoroutine(EatFood(other.gameObject));
			}
			else if (other.CompareTag("Toy")) {
				GiveObjectToKid(other.gameObject);
				StartCoroutine(PlayToy(other.gameObject));
			}
		}
	}


	private void GiveObjectToKid (GameObject objectToGive) {
		objectToGive.transform.parent = handSlot.transform;
		objectToGive.transform.position = handSlot.transform.position;
		objectToGive.transform.rotation = Quaternion.identity;

		aggroMan.kidAggro = KidAggroManager.aggroState.Interacting;
	}


	IEnumerator EatFood (GameObject pickup) {
		///Takes a few seconds to eat the food
		yield return new WaitForSeconds(3f);

		GameSceneManager.AlterBloodSugar(pickup.GetComponent<PickupInteractable>().bloodSugarValue);
		Destroy(pickup);
		interacting = false;
		aggroMan.DetermineAggroState();
	}


	IEnumerator PlayToy (GameObject pickup) {
		///plays with the toy until he becomes bored
		GameSceneManager.bloodSugarStabilzed = true;

		yield return new WaitForSeconds(pickup.GetComponent<PickupInteractable>().stabilizeDuration);

		GameSceneManager.bloodSugarStabilzed = false;
		Destroy(pickup);
		interacting = false;
		aggroMan.DetermineAggroState();
	}


	
}
