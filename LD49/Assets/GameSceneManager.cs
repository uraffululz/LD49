using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	UIManager UIMan;

	public bool sceneActive = true;

	float timerMax = 120f;
	float currentTimeLeft;

	public static float currentBloodSugar = 1f;
	public static bool bloodSugarStabilzed = false;

	public int totalDamageCaused = 0;



	void Awake () {
		UIMan = GetComponent<UIManager>();
		//Cursor.visible = false;

		currentTimeLeft = timerMax;
	}



	void Update() {
		if (sceneActive) {
			if (currentTimeLeft > 0) {
				currentTimeLeft -= Time.deltaTime;
				UIMan.AlterTimerBar(currentTimeLeft/timerMax);
			}
			else {
				LastedToParents();
			}
		}
    }


	public static void AlterBloodSugar (float amount) {
		currentBloodSugar += amount;
		currentBloodSugar = Mathf.Clamp(currentBloodSugar, 0, 1);
		//print("Clamped blood sugar: " + currentBloodSugar);
	}


	public void AlterDamagesCaused(int damages) {
		totalDamageCaused += damages;
		UIMan.AlterDamagesText();
	}


	public void StartGame() {
		sceneActive = true;
		UIMan.DisableStartUI();
	}

	
	public void ReturnToMenu() {
		SceneManager.LoadScene(0);
	}


	public void QuitGame() {
		Application.Quit();
	}


	public void LastedToParents () {
		sceneActive = false;
		UIMan.EnableEndUI();
	}


	public void LoseToDeath () {
		sceneActive = false;
		UIMan.EnableDeadUI();
	}
}
