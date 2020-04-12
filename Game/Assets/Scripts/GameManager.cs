using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	const string LEVEL_KEY = "GameManager.currLevel";
	public static GameManager instance = null;

	public int currLevel = 0;

	bool isPlaying = false;

	private void Awake() {
		currLevel = PlayerPrefs.GetInt(LEVEL_KEY, 1);

		instance = this;
	}

	void Update() {
	
	}

	public void StartGame() {
		isPlaying = true;
	}

	public void OnWin() {
		isPlaying = true;
		PlayerPrefs.SetInt(LEVEL_KEY, ++currLevel);
	}

	public void OnLose() {
		isPlaying = true;
	}
}
