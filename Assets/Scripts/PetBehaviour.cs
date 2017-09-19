using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//総移動距離により、ペットの体型を変化させる。
public class PetBehaviour : MonoBehaviour
{

	public bool isAR;  // ARモードかどうか

	// Use this for initialization
	void Start ()
	{
		Changebody ();
	}

	public void Changebody () {
		if (isAR) {
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, SaveData.Instance.GetSize() * 0.4f);
		} else {
			gameObject.GetComponent<Pet> ().animeTime = SaveData.Instance.GetSize ();
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, SaveData.Instance.GetSize());
		}
	}
}
