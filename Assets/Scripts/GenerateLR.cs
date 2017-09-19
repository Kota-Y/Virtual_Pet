using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLR : MonoBehaviour {

	private GameObject lineGroup; // for grouping
	private float[] xArray;
	private DateTime dt;
	[SerializeField] private Text monthText;
	[SerializeField] private Text[] distanceTexts;

	void Start () {
		print (SaveData.Instance.TotalDistance());
		lineGroup = new GameObject ("LineGroup");
		lineGroup.transform.parent = gameObject.transform;
		dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
		for (int i = 0; i < distanceTexts.Length; i++) {
			distanceTexts [i].text = (i*2).ToString ();
			distanceTexts [i].gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-340f, -234f + 55f * i);
		}
		OnClickButton (0);
	}

	public void OnClickButton(int num) { // -1:先月, 0:今月, 1:来月
		foreach (Transform child in lineGroup.transform) {
			Destroy (child.gameObject);
		}
		dt = new DateTime (dt.AddMonths(num).Year, dt.AddMonths(num).Month, dt.AddMonths(num).Day);
		monthText.text = dt.Year.ToString() + "年 " + dt.Month.ToString() + "月";
		int days = DateTime.DaysInMonth (dt.Year, dt.Month);
		xArray = new float[days];
		for (int i = 0; i < xArray.Length; i++) {
			xArray [i] = -2.5f + 0.18f * i;
		}

		List<Vector2> my2DPoint = new List<Vector2> ();
		for (int i = 0; i < days; i++) {
			my2DPoint.Add (new Vector2 (xArray[i], -1f + 0.25f * SaveData.Instance.LoadDistance(dt.Year, dt.Month, i+1)));
		}

		for (int i = 0; i < my2DPoint.Count - 1; i++) {
			DrawLine (my2DPoint, /* startPos=*/i);
		}
	}

	void DrawLine(List<Vector2> my2DVec, int startPos) {
		List<Vector3> myPoint = new List<Vector3>();
		for(int i=0; i<2; i++) {
			myPoint.Add(new Vector3(my2DVec[startPos+i].x, my2DVec[startPos+i].y, 0.0f));
		}

		GameObject newLine = new GameObject ("Line" + startPos.ToString() );
		LineRenderer lRend = newLine.AddComponent<LineRenderer> ();
		lRend.SetVertexCount(2);
		lRend.SetWidth (0.05f, 0.05f);
		Vector3 startVec = myPoint[0];
		Vector3 endVec   = myPoint[1];
		lRend.SetPosition (0, startVec);
		lRend.SetPosition (1, endVec);

		newLine.transform.parent = lineGroup.transform; // for grouping
	}
}
