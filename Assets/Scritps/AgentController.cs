using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour {

	static private List<AgentController> agentControllers;

	static public AgentController Spwan ( Vector3 location, Vector3 heading ){
		foreach(AgentController agentController in agentControllers){
			if(agentController.gameObject.activeSelf == false) {
				agentController.transform.position = location;
				agentController.transform.eulerAngles = heading;

				agentController.gameObject.SetActive(true);

				return agentController;
			}
		}
		return null;
	}

	void Awake() {
		if(agentControllers == null ){
			agentControllers = new List<AgentController>();
		}
		agentControllers.Add(this);
	}

	void OnDestroy()
	{
		agentControllers.Remove(this);
		if(agentControllers.Count == 0){
			agentControllers = null;
		}
	}

	void Start () {
		gameObject.SetActive(false) ;
	}
	
	void Update () {
		transform.position += transform.up * (Time.deltaTime * 4);
	}

	void OnBecameInvisible()
	{
		gameObject.SetActive(false);
	}
}
