﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	private static MainController mainController;

	private string currentSceneName;
	private string nextSceneName;
	private AsyncOperation resourceUnloadTask;
	private AsyncOperation sceneLoadTask;
	private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };
	private SceneState sceneState;
	private delegate void UpdateDelegate();
	private UpdateDelegate[] updateDelegates;

	public static void SwitchScene(string nextSceneName){
		if(mainController != null){
			if(mainController.currentSceneName != nextSceneName){
				mainController.nextSceneName = nextSceneName;
			}
		}
	}

	protected void Awake()
	{
		Object.DontDestroyOnLoad(gameObject);

		mainController = this;

		updateDelegates = new UpdateDelegate[(int)SceneState.Count];

		updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
		updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
		updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
		updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
		updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
		updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
		updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

		nextSceneName = "Menu Scene";
		sceneState = SceneState.Reset;

	}

	protected void OnDestroy()
	{
		if(updateDelegates != null){
			for (int i = 0; i < (int)SceneState.Count; i++)
			{
				updateDelegates[i] = null;
			}
			updateDelegates = null;
		}

		if(mainController != null){
			mainController = null;
		}
	}
	
	void Update () {
		if(updateDelegates[(int)sceneState] != null){
			updateDelegates[(int)sceneState]();
		}
	}

	private void UpdateSceneReset(){
		System.GC.Collect();
		sceneState = SceneState.Preload;
	}

	private void UpdateScenePreload() {
		sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
		sceneState = SceneState.Load;
	}

	private void UpdateSceneLoad() {
		if(sceneLoadTask.isDone == true){
			sceneState = SceneState.Unload;
		} else {
			// update Scene loading progress
		}
	}

	private void UpdateSceneUnload() {
		if(resourceUnloadTask == null){
			resourceUnloadTask = Resources.UnloadUnusedAssets();
		} else {
			if(resourceUnloadTask.isDone == true){
				resourceUnloadTask = null;
				sceneState = SceneState.Postload;
			}
		}
	}

	private void UpdateScenePostload() {
		currentSceneName = nextSceneName;
		sceneState = SceneState.Ready;
	}

	private void UpdateSceneReady() {
		System.GC.Collect();
		sceneState = SceneState.Run;
	}

	private void UpdateSceneRun() {
		if(currentSceneName != nextSceneName) {
			sceneState = SceneState.Reset;
		}
	}
}
