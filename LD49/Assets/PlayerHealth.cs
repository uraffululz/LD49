using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
 
	[SerializeField] GameSceneManager sceneMan;

	[SerializeField] int currentHP = 5;


    void Start() {
        
    }

   

    void Update() {
        
    }


	public void TakeDamage(int damage) {
		currentHP -= damage;
		if (currentHP <= 0) {
			sceneMan.LoseToDeath();
		}
	}


}
