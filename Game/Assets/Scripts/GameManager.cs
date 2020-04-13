using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	const string LEVEL_KEY = "GameManager.currLevel";
	public static GameManager instance = null;

	[System.NonSerialized] public int currLevel = 0;

	[SerializeField] LineRenderer shootLine;
	[SerializeField] LayerMask hitMask;
	[SerializeField] Player player;
	[SerializeField] MainMenu mainMenu;

	bool isPlaying = false;

	float holdTime = 0;
	Vector3 lastTouchPos;

	private void Awake() {
		currLevel = PlayerPrefs.GetInt(LEVEL_KEY, 1);
		shootLine.gameObject.SetActive(false);

		instance = this;
	}

	void Update() {
		if (!isPlaying || !player.isCanShoot)
			return;

		if (Input.GetMouseButtonDown(0)) {
			shootLine.gameObject.SetActive(true);
			holdTime = 0.0f;

			player.InitShoot();
		}
		if (Input.GetMouseButton(0)) {
			holdTime += Time.deltaTime;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 50f, hitMask)) {
				lastTouchPos = new Vector3(hit.point.x, 0.01f, hit.point.z);
				shootLine.SetPosition(1, lastTouchPos);
			}

			player.OnHold(lastTouchPos, holdTime);
		}
		else if (Input.GetMouseButtonUp(0)) {
			shootLine.gameObject.SetActive(false);
			player.ShootTo(lastTouchPos, holdTime);
		}
	}

	public void StartGame() {
		LeanTween.delayedCall(0.2f, () => {
			isPlaying = true;
		});
	}

	public void OnWin() {
		isPlaying = false;
		PlayerPrefs.SetInt(LEVEL_KEY, ++currLevel);
		mainMenu.Show();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnLose() {
		isPlaying = false;
		mainMenu.Show();
		Application.LoadLevel(Application.loadedLevel);
	}
}
