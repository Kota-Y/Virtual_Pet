using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {

	public PetBehaviour petBehaviour;
	public GenerateLR generateLR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 8月のデータをランダム生成、9月の一部も
	public void CreateAugust () {
		for (int i = 1; i <= 31; i++) {
			SaveData.Instance.Save (2017, 8, i, (float)Random.Range(0, 20));
		}
		for (int i = 1; i <= 10; i++) {
			SaveData.Instance.Save (2017, 9, i, (float)Random.Range(0, 20));
		}
		generateLR.OnClickButton (0);
		petBehaviour.Changebody ();
	}

	// 激太りさせて殺す
	public void GrowFat () {
		petBehaviour.gameObject.GetComponent<PetController> ().demoFlag = true;
		petBehaviour.gameObject.transform.localScale = new Vector3 (0.5f, 0.5f, 2.5f);
		petBehaviour.gameObject.GetComponent<Pet> ().animeTime = 2.5f;
		petBehaviour.gameObject.GetComponent<Pet> ().Die ();
	}
}
