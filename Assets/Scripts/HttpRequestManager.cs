using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HttpRequestManager : MonoBehaviour
{
	// URL
	string url = "http://fullfill.sakura.ne.jp/vp/stoc.php";
	// サーバへリクエストするデータ
	string id = "256";
	string distance = "10";
	// タイムアウト時間
	float timeoutsec = 5f;

	//distance情報を持っているクラスを定義
	/*
	void Start ()
	{
		//distance = d2s (distanceデータ);

		// サーバへPOSTするデータを設定 
		Dictionary<string, string> dic = new Dictionary<string, string> ();
		dic.Add ("id", id);
		dic.Add ("distance", distance);
		StartCoroutine (HttpPost (url, dic));  // POST

		// サーバへGETするデータを設定
		string get_param = "?id=" + id + "&distance=" + distance;
		StartCoroutine (HttpGet (url + get_param));  // GET
	}
	*/

	public void StartHttp ()
	{
		//distance = d2s (distanceデータ);

		// サーバへPOSTするデータを設定 
		Dictionary<string, string> dic = new Dictionary<string, string> ();
		dic.Add ("id", id);
		dic.Add ("distance", distance);
		StartCoroutine (HttpPost (url, dic));  // POST

		// サーバへGETするデータを設定
		string get_param = "?id=" + id + "&distance=" + distance;
		StartCoroutine (HttpGet (url + get_param));  // GET
	}

	//float to string
	string f2s (float dist)
	{
		return dist.ToString ();
	}

	// HTTP POST リクエスト
	IEnumerator HttpPost (string url, Dictionary<string, string> post)
	{
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<String, String> post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);
		}
		WWW www = new WWW (url, form);

		// CheckTimeOut()の終了を待つ。5秒を過ぎればタイムアウト
		yield return StartCoroutine (CheckTimeOut (www, timeoutsec));

		if (www.error != null) {
			Debug.Log ("HttpPost NG: " + www.error);
		} else if (www.isDone) {
			// サーバからのレスポンスを表示
			Debug.Log ("HttpPost OK: " + www.text);
		}
	}

	// HTTP GET リクエスト
	IEnumerator HttpGet (string url)
	{
		WWW www = new WWW (url);

		// CheckTimeOut()の終了を待つ。5秒を過ぎればタイムアウト
		yield return StartCoroutine (CheckTimeOut (www, timeoutsec));

		if (www.error != null) {
			Debug.Log ("HttpGet NG: " + www.error);
		} else if (www.isDone) {
			// サーバからのレスポンスを表示
			Debug.Log ("HttpGet OK: " + www.text);
		}
	}

	// HTTPリクエストのタイムアウト処理
	IEnumerator CheckTimeOut (WWW www, float timeout)
	{
		float requestTime = Time.time;

		while (!www.isDone) {
			if (Time.time - requestTime < timeout)
				yield return null;
			else {
				Debug.Log ("TimeOut");  //タイムアウト
				break;
			}
		}
		yield return null;
	}
}
