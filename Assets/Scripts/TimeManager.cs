using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	// シングルトンにする  ------------------------------
	public static TimeManager Instance {
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

	[SerializeField] private Text buttonText;

	// タイムのプロパティ
	[SerializeField] private Text timeText;
	private float _time;
	public float time {
		get { return _time;}
		set {
			_time = value;
			timeText.text = _time.ToString ("F2");
		}
	}
	private bool flag;

	void Start () {
		flag = false;
	}

	void Update () {
		if (flag) {
			time += Time.deltaTime;
		}
	}

	public void OnClickStartButton() {
		if (flag) {
			flag = false;
			buttonText.text = "スタート";
		} else {
			flag = true;
			buttonText.text = "ストップ";
		}
	}

	public void OnClickResetButton () {
		flag = false;
		time = 0;
	}
}
