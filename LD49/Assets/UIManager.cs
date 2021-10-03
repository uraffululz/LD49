using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameSceneManager sceneMan;

	[SerializeField] GameObject startBG;
	[SerializeField] GameObject endBG;
	[SerializeField] GameObject deadBG;
 
	[SerializeField] Text interactText;

	[SerializeField] Image bloodMeter;
	float bloodSugarDrainRate = .05f;

	[SerializeField] Image timerBar;

	[SerializeField] Text damagesText;

	[SerializeField] Image crosshair;


    void Awake() {
        sceneMan = GetComponent<GameSceneManager>();
    }

   

    void Update() {
		if (!GameSceneManager.bloodSugarStabilzed && sceneMan.sceneActive) {
		GameSceneManager.AlterBloodSugar(-bloodSugarDrainRate * Time.deltaTime);
		AlterBloodMeter(GameSceneManager.currentBloodSugar);
		}
    }


	public void DisplayInteraction (string interaction) {
		interactText.text = interaction;
	}

	public void ClearInteraction() {
		interactText.text = "";
	}


	public void AlterBloodMeter (float fill) {
		bloodMeter.fillAmount = fill;
		//GameSceneManager.currentBloodSugar = bloodMeter.fillAmount;
	}


	public void AlterDamagesText() {
		damagesText.text = "Total Damages: $" + sceneMan.totalDamageCaused;
	}


	public void AlterTimerBar(float amount) {
		//float timerScaleX = amount;
		timerBar.fillAmount = amount;
	}


	public void DisableStartUI() {
		startBG.SetActive(false);
		crosshair.gameObject.SetActive(true);
	}


	public void EnableEndUI () {
		endBG.SetActive(true);
		crosshair.gameObject.SetActive(false);

	}


	public void EnableDeadUI () {
		deadBG.SetActive(true);
		crosshair.gameObject.SetActive(false);

	}
}
