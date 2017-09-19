using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetController : MonoBehaviour
{

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;
	private string Direction;
	public Pet pet;
	public ParticleSystem particle;
	public bool demoFlag = false;

	void Update ()
	{
		if (Time.timeScale > 0) {
			Flick ();
		}
	}



	/// タップ＆フリックでの処理

	void Flick ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			touchStartPos = new Vector3 (Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);
		}

		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			touchEndPos = new Vector3 (Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);

			GetDirection ();
		}
	}

	void GetDirection ()
	{
		float directionX = touchEndPos.x - touchStartPos.x;
		float directionY = touchEndPos.y - touchStartPos.y;


		if (Mathf.Abs (directionY) < Mathf.Abs (directionX)) {
			if (30 < directionX) {
				//右向きにフリック
				Direction = "right";
			} else if (-30 > directionX) {
				//左向きにフリック
				Direction = "left";
			}
		} else if (Mathf.Abs (directionX) < Mathf.Abs (directionY)) {
			if (30 < directionY) {
				//上向きにフリック
				Direction = "up";
			} else if (-30 > directionY) {
				//下向きのフリック
				Direction = "down";
			}
		} else {
			//タッチを検出
			Direction = "touch";
		}
		switch (Direction) {
		case "up":
				//上フリックされた時の処理
			pet.Flip (1);  // バク宙
			break;

		case "down":
				//下フリックされた時の処理
			pet.Flip (-1);  // 前宙
			break;

		case "right":
			pet.SpinJump (-1);  // 左回り
			break;

		case "left":
				//左フリックされた時の処理
			pet.SpinJump (1);  // 右回り
			break;

		case "touch":
				//タッチされた時の処理
			pet.Jump ();  // ジャンプ
			particle.Play ();
			break;
		}
	}

	void PlaySE ()
	{
		AudioClip clip = gameObject.GetComponent<AudioSource> ().clip;
		gameObject.GetComponent<AudioSource> ().PlayOneShot (clip);
	}

}
