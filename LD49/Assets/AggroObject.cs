using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroObject : MonoBehaviour {
	[SerializeField] GameSceneManager sceneMan;

	[SerializeField] GameObject kid;

	[SerializeField] GameObject damagedVersion;
 
	[SerializeField] int maxDurability;
	[SerializeField] int durability;

	[SerializeField] int moneyValue;


    void Start() {
        durability = maxDurability;
    }

   

    void Update() {
        
    }


	public void TakeDamage(int damage) {
		sceneMan.AlterDamagesCaused(moneyValue/maxDurability);
		durability -= damage;

		if (durability > 0) {
		//}
		//else {
			///Replace the object with its "damaged" version
			Instantiate(damagedVersion, transform.position, Quaternion.identity);
			///Remove object from the Kid's "aggro objects" array
			kid.GetComponent<KidMove>().aggroObjects.Remove(this.gameObject);
			///Have the Kid find his next target
			kid.GetComponent<KidAggroManager>().DetermineAggroState();
			///Destroy this gameObject
			Destroy(this.gameObject);
		}
	}
}
