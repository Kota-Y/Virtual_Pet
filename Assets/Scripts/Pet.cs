using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pet : MonoBehaviour
{

	private Vector3 defaultRotation = new Vector3 (0f, 110f, 0f);
	private bool moveFlag = false;
	public float animeTime = 1.0f;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(defaultRotation.x, defaultRotation.y, defaultRotation.z);
	}

	//	// Update is called once per frame
	//	void Update () {
	//		if(OnTouchDown()){
	//			Debug.Log("タップされました");
	//			Die();
	//		}
	//	}
	//
	//	//スマホ向け そのオブジェクトがタッチされていたらtrue（マルチタップ対応）
	//	bool OnTouchDown() {
	//		// タッチされているとき
	//		if( 0 < Input.touchCount){
	//			// タッチされている指の数だけ処理
	//			for(int i = 0; i < Input.touchCount; i++){
	//				// タッチ情報をコピー
	//				Touch t = Input.GetTouch(i);
	//				// タッチしたときかどうか
	//				if(t.phase == TouchPhase.Began ){
	//					//タッチした位置からRayを飛ばす
	//					Ray ray = Camera.main.ScreenPointToRay(t.position);
	//					RaycastHit hit = new RaycastHit();
	//					if (Physics.Raycast(ray, out hit)){
	//						//Rayを飛ばしてあたったオブジェクトが自分自身だったら
	//						if (hit.collider.gameObject == this.gameObject){
	//							return true;
	//						}
	//					}
	//				}
	//			}
	//		}
	//		return false; //タッチされてなかったらfalse
	//	}

	public void Jump () {
		if (moveFlag) {return;}
		moveFlag = true;
		transform.DOLocalJump(
			transform.position, // 移動終了地点
			1,                // ジャンプする力
			2,                // ジャンプする回数
			animeTime         // アニメーション時間
		).OnComplete(
			() => {moveFlag = false;
			}).SetEase(Ease.Linear);
	}

	public void SpinJump (int direction) {  // 1:右回り, -1:左周り
		if (moveFlag) {return;}
		moveFlag = true;
		Sequence sequence = DOTween.Sequence ();
		sequence.Append (transform.DOLocalJump(transform.position, 1, 3, animeTime * 1.5f).SetEase(Ease.Linear));
		sequence.Join (transform.DOLocalRotate (new Vector3(0, defaultRotation.y + 360f * direction), animeTime * 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear)
		).OnComplete(() => {moveFlag = false;});
	}

	public void Flip (int direction) {  // 1:バク宙, -1,前宙
		if (moveFlag) {return;}
		moveFlag = true;
		Sequence sequence = DOTween.Sequence ();
		sequence.Append (transform.DOLocalJump(transform.position, 2, 1, animeTime).SetEase(Ease.Linear));
		sequence.Join (transform.DOLocalRotate (new Vector3(0f, defaultRotation.y, 360f * direction), animeTime, RotateMode.FastBeyond360).SetEase(Ease.InOutExpo)
			.OnComplete(() => {moveFlag = false;}));
	}

	public void Die () {
		if (moveFlag) {return;}
		moveFlag = true;
		transform.rotation = Quaternion.Euler(defaultRotation.x, defaultRotation.y, defaultRotation.z);
		transform.DOLocalRotate (new Vector3 (-90f, defaultRotation.y, 0f), animeTime * 1f).SetEase (Ease.OutBounce)
			.OnComplete(() => {
				moveFlag = false;
				GetComponent<PetController>().demoFlag = false;
//				StartCoroutine(AfterDie());
			});
	}

//	// 死んだら2秒後に起こさせる
//	public IEnumerator AfterDie () {
//		yield return new WaitForSeconds (2f);
//		transform.rotation = Quaternion.Euler(defaultRotation.x, defaultRotation.y, defaultRotation.z);
//	}
}
