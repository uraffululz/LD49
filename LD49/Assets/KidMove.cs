using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KidMove : MonoBehaviour {

	[SerializeField] GameObject player;

	[SerializeField] GameSceneManager sceneMan;
	KidAggroManager aggroMan;

	//[SerializeField] NavManager navMan;
	[SerializeField] Transform TVNode;
	[SerializeField] Transform[] wanderingNodes;
	public List<GameObject> aggroObjects;
 
	NavMeshAgent navAgent;
	[SerializeField] Transform currentNavTarget;
	float approachDist = 2f;


    void Awake() {
        navAgent = GetComponent<NavMeshAgent>();

		aggroMan = GetComponent<KidAggroManager>();
    }


	void Start () {
		navAgent.destination = TVNode.transform.position;
	}


	void Update() {
		if (sceneMan.sceneActive && navAgent.isOnNavMesh) {
			switch (aggroMan.kidAggro) {
				case KidAggroManager.aggroState.Calm:
					if (Vector3.Distance(transform.position, TVNode.transform.position) <= approachDist) {
						///Sit and watch TV
						aggroMan.DetermineAggroState();
					}
					break;
				case KidAggroManager.aggroState.Interacting:
					///Stop to interact with whatever pickup the kid is holding
					currentNavTarget = transform;
					navAgent.destination = currentNavTarget.position;
					break;
				case KidAggroManager.aggroState.Wandering:
					if (Vector3.Distance(transform.position, navAgent.destination) <= approachDist) {
						aggroMan.DetermineAggroState();
					}
					break;
				case KidAggroManager.aggroState.FullAggro:
					if (navAgent.destination != null) {
						if (Vector3.Distance(transform.position, navAgent.destination) <= approachDist) {
							StartCoroutine(aggroMan.Thrash());
						}
					}
					else {
						aggroMan.DetermineAggroState();
					}
					break;
				default:
					print("Not sure what to do here");
					break;
			}
		}
		else {
			//print("I'm off the nav mesh!");
		}
    }


	public void AcquireNewNavTarget(KidAggroManager.aggroState aggroLevel) {
		currentNavTarget = transform;

		switch (aggroLevel) {
			case KidAggroManager.aggroState.Calm:
				currentNavTarget = TVNode.transform;
				break;
			case KidAggroManager.aggroState.Interacting:

				break;
			case KidAggroManager.aggroState.Wandering:
				currentNavTarget = wanderingNodes[Random.Range(0, wanderingNodes.Length)].transform;
				break;
			case KidAggroManager.aggroState.FullAggro:
				if (aggroObjects.Count > 0) {
					aggroMan.currentAggroTarget = aggroObjects[Random.Range(0, aggroObjects.Count)];
					currentNavTarget = aggroMan.currentAggroTarget.transform;
				}
				else if (player.activeInHierarchy) {
					aggroMan.currentAggroTarget = player;
					currentNavTarget = aggroMan.currentAggroTarget.transform;
				}
				else {
					navAgent.isStopped = true;
				}
				break;
			default:
				print("Not sure where this kid is going...");
				break;
		}

		navAgent.SetDestination(currentNavTarget.position);
	}
}
