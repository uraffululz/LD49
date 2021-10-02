using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAggroManager : MonoBehaviour {

	[SerializeField] GameObject player;

	KidMove moveScript;
	///[SerializeField] Animator anim;
 
	public enum aggroState {Calm, Interacting, Wandering, FullAggro};
	public aggroState kidAggro;

	public GameObject currentAggroTarget;
	int damage = 1;


    void Awake() {
        moveScript = GetComponent<KidMove>();
    }

   
    void Update() {
        
    }


	public void DetermineAggroState () {
		if (GameSceneManager.currentBloodSugar <= .1f) {
			print("Kid is full aggro");
			kidAggro = KidAggroManager.aggroState.FullAggro;
			moveScript.AcquireNewNavTarget(kidAggro);
		}
		else if (GameSceneManager.currentBloodSugar <= .5f) {
			print("Kid is wandering");
			kidAggro = KidAggroManager.aggroState.Wandering;
			moveScript.AcquireNewNavTarget(kidAggro);
		}
		else {
			kidAggro = KidAggroManager.aggroState.Calm;
			moveScript.AcquireNewNavTarget(kidAggro);
		}
	}


	public IEnumerator Thrash() {
		Vector3 lookPos = new Vector3(currentAggroTarget.transform.position.x, transform.position.y, currentAggroTarget.transform.position.z);
		transform.LookAt(lookPos, Vector3.up);
		///anim.SetBool("attacking", true);
		yield return new WaitForSeconds(1f);

		DealDamage();
	}


//TODO Make this an animation event on the Kid's attack animation
	void DealDamage() {
		if (currentAggroTarget == player) {
			currentAggroTarget.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
		else {
			currentAggroTarget.GetComponent<AggroObject>().TakeDamage(damage);
		}
	}
}
