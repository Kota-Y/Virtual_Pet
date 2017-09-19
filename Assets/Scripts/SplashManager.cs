using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadSceneAsync ("MainScene");
	}
}
