using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {
	[SerializeField] TextMeshProUGUI levelTextField = null;

	[HideInInspector] [SerializeField] CanvasGroup cg = null;

#if UNITY_EDITOR
	private void OnValidate() {
		if (cg == null)
			cg = GetComponent<CanvasGroup>();
	}
#endif

	private void Start() {
		Show();
	}

	public void OnPlayClick() {
		Hide();
		GameManager.instance.StartGame();
	}

	public void Show() {
		levelTextField.text = $"Level: {GameManager.instance.currLevel}";
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 1.0f, 0.2f);
	}

	public void Hide() {
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 0.0f, 0.2f);
	}
}
