using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static GameController gameController;

	void Awake()
	{
		gameController = this;
	}

	void OnDestroy()
	{
		if(gameController != null)	{
			gameController = null;
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) == true) {
			MainController.SwitchScene("Menu Scene");
		}
	}
}
