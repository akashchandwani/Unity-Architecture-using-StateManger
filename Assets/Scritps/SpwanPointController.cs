using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPointController : MonoBehaviour {

	private static SpwanPointController spwanPointController;

	void Awake()
	{
		spwanPointController = this;
	}
	
	void OnDestroy()
	{
		if(spwanPointController != null) {
			spwanPointController = null;
		}
	}

	void Update () {
		transform.Rotate(Vector3.forward, Time.deltaTime * 60.0f);

		if(Input.GetKeyDown(KeyCode.Space) == true){
			AgentController.Spwan(transform.position, transform.eulerAngles);
		}
	}
}
