using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {

	[SerializeField] GameSceneManager sceneMan;
	[SerializeField] UIManager UIMan;
 
	[SerializeField] GameObject[] pickups;

	[SerializeField] Transform[] spawnPoints;
	List<Transform> availableSpawnPoints;

	float spawnTimer = 20;
	float currentSpawnTime;


    void Start() {
		SpawnPickup(6);
    }

   

    void Update() {
		if (sceneMan.sceneActive) {
			if (currentSpawnTime > 0) {
				currentSpawnTime -= Time.deltaTime;
			}
			else {
				SpawnPickup(1);
				currentSpawnTime = spawnTimer;
			}
		}
    }


	void SpawnPickup(int howMany) {
		availableSpawnPoints = new List<Transform>();
		foreach (Transform point in spawnPoints) {
			if (point.childCount == 0) {
				availableSpawnPoints.Add(point);
			}
		}

		int spawnsActual = Mathf.Min(howMany, availableSpawnPoints.Count);
		print("Actual spawns: " + spawnsActual);

		for (int i = 0; i < spawnsActual; i++) {
			Transform newSpawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
			GameObject spawnedPickup = Instantiate(pickups[Random.Range(0, pickups.Length)], newSpawnPoint.transform.position, Quaternion.identity, newSpawnPoint);
			spawnedPickup.GetComponent<PickupInteractable>().UIMan = UIMan;

			availableSpawnPoints.Remove(newSpawnPoint);
		}
		
	}
}
