using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	private static MenuController menuController;

	protected void Awake() {
		menuController = this;
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		if(menuController != null){
			menuController = null;
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(Input.GetMouseButton(0) == true) {
			MainController.SwitchScene("Game Scene");
		}
	}
	
}
