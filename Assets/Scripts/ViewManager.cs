using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour {

	// シングルトンにする  ------------------------------
	public static ViewManager Instance {
		get; private set;
	}

	void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
//		DontDestroyOnLoad (gameObject);
	}
	//  ------------------------------------------------------------

	[SerializeField] private GameObject[] view;  // 0:ペット , 1:マップ , 2: グラフ
	[SerializeField] private RectTransform rectTras;

	void Start () {
		ChangeView (1);
	}

	public void ChangeView (int num) {
		for (int i = 0; i < view.Length; i++) {
			if (i == num) {
				view [i].SetActive (true);
				rectTras.anchoredPosition = new Vector2 (-240f + i * 240f, -540f);
			} else {
				view [i].SetActive (false);
			}
		}
	}
}
