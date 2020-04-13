using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	const string LEVEL_KEY = "GameManager.currLevel";
	public static GameManager instance = null;

	[System.NonSerialized] public int currLevel = 0;
	[System.NonSerialized] public List<Enemy> enemies = new List<Enemy>();

	public PathLine pathLine;

	[SerializeField] LineRenderer shootLine;
	[SerializeField] LayerMask hitMask;
	[SerializeField] Player player;
	[SerializeField] MainMenu mainMenu;
	[SerializeField] Transform[] jumpPos;

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

		if(pathLine.EnemiesCount == 0) {
			OnWin();
		}

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
				pathLine.SetWidth(player.GetPathWidth());
				shootLine.startWidth = shootLine.endWidth = player.GetBulletWidth();
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
		shootLine.gameObject.SetActive(false);
		player.ShootTo(lastTouchPos, holdTime);
		PlayerPrefs.SetInt(LEVEL_KEY, ++currLevel);

		foreach (var enemy in enemies)
			enemy.OnWin();

		for(int i = 1; i < jumpPos.Length; ++i) {
			int curr = i;
			LeanTween.value(0.0f, 1.0f, 1.0f)
			.setDelay(1.0f * curr)
			.setOnUpdate((float f) => {
				Vector3 newPos = Vector3.Lerp(jumpPos[curr - 1].position, jumpPos[curr].position, f);
				if(f < 0.5f)
					newPos.y = Mathf.Lerp(jumpPos[curr - 1].position.y, jumpPos[curr].position.y + 3.0f, f * 2f);
				else
					newPos.y = Mathf.Lerp(jumpPos[curr].position.y + 3.0f, jumpPos[curr].position.y, (f - 0.5f) * 2f);
				newPos.y = Mathf.Sqrt(newPos.y);
				player.transform.position = newPos;
			});
		}

		LeanTween.delayedCall(7.0f, () => {
			Application.LoadLevel(Application.loadedLevel);
		});
	}

	public void OnLose() {
		isPlaying = false;
		Application.LoadLevel(Application.loadedLevel);
	}
}
