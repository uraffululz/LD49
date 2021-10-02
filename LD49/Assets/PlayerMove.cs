using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

	GameObject mainCam;

	[SerializeField] GameSceneManager sceneMan;
 
	float moveSpeed = 4f;
	Vector3 moveDir;

	float rotXSpeed = 12f;
	float camRotYSpeed = .25f;
	float mouseX;
	float mouseY;

	float rotationX = 0;



    void Awake() {
        mainCam = Camera.main.gameObject;
		
    }
   

    void Update() {
		if (sceneMan.sceneActive) {
			Vector3 movePos = (transform.right * moveDir.x) + (transform.forward * moveDir.z);

			transform.position += movePos * moveSpeed * Time.deltaTime;
			transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

			rotationX += mouseY;
			rotationX = Mathf.Clamp(rotationX, -80, 80);
			Vector3 camRotation = transform.eulerAngles;
			camRotation.x -= rotationX;
			mainCam.transform.eulerAngles = camRotation;
		}
	}


	void OnMove (InputValue value) {
		moveDir = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
	}


	public void MouseInput(Vector2 input) {
		mouseX = input.x * rotXSpeed;
		mouseY = input.y * camRotYSpeed;
	}

	void OnRotateX(InputValue deltaX) {
		mouseX = deltaX.Get<float>() * rotXSpeed;
	}


	void OnRotateY (InputValue deltaY) {
		mouseY = deltaY.Get<float>() * camRotYSpeed;
	}
}
