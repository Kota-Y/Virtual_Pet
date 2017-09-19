using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Item {
	public string id;
	public string password;
}

public class SubmitManager : MonoBehaviour {
	string id;
	string password;
	[SerializeField] private InputField inputField1;
	[SerializeField] private InputField inputField2;
	[SerializeField] private Text msgText;

	// ログインボタンが押されたとき
	public void OnClickButton() {
		StartCoroutine(Connect());
	}

	private IEnumerator Connect() {
		string url = "";
		string msg = "";
		id = inputField1.text;  // 入力されたユーザ名を格納
		password = inputField2.text;  // 入力されたパスワードを格納
		if (id == "") {
			msg += "IDを入力してください\n";
		}
		if (password == "") {
			msg += "パスワードを入力してください\n";
		}
		msgText.text = msg;
		if (msg != "") {
			yield break;
		}

		string itemJson = "{ \"id\": \"id\", \"password\": \"password\" }";
		Item item = JsonUtility.FromJson<Item>(itemJson);
		Debug.Log("item id " + item.id);
		Debug.Log("item password " + item.password);

		SceneManager.LoadSceneAsync ("MainScene");

		yield return null;

//		WWWForm form = new WWWForm(); 
//		form.AddField("Text1", str1);
//		form.AddField("Text2", str2);
//		WWW www = new WWW(url, form);
//		yield return www;
//
//		if (www.error == null) {
//			Debug.Log("WWW Submit Ok!: " + www.text);
//		} else {
//			Debug.Log("WWW Submit Error: " + www.error);
//		}
	}
}

