using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  // CSVファイルを扱えるようにするため
using System;

public class SaveData : MonoBehaviour {

	// シングルトンにする  ------------------------------
	public static SaveData Instance {
		get; private set;
	}

	void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad (gameObject);
	}
	//  ------------------------------------------------------------

	private string filePath;
	private List<string[]> dataList = new List<string[]>();

	void Start () {
		filePath = Application.dataPath + "/Resources/SaveData.csv";
		ReadCSV ();
	}

	public void Save (float distance) {
		bool flag = false;
		int index = 0;
		for (int i = 0; i < dataList.Count; i++) {
			if (int.Parse(dataList[i][0]) == DateTime.Today.Year && int.Parse(dataList[i][1]) == DateTime.Today.Month && int.Parse(dataList[i][2]) == DateTime.Today.Day) {
				dataList[i][3] = (float.Parse(dataList[i][3]) + distance).ToString();
				index = i;
				flag = true;
				break;
			}
		}
		if (!flag) {
			dataList.Add (new string[] {DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Day.ToString(), distance.ToString()});	
		}
		WriteCSV (DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, float.Parse(dataList[index][3]));
	}

	public void Save (int year, int month, int day, float distance) {
		bool flag = false;
		int index = 0;
		for (int i = 0; i < dataList.Count; i++) {
			if (int.Parse(dataList[i][0]) == year && int.Parse(dataList[i][1]) == month && int.Parse(dataList[i][2]) == day) {
				dataList[i][3] = (float.Parse(dataList[i][3]) + distance).ToString();
				index = i;
				flag = true;
				break;
			}
		}
		if (!flag) {
			dataList.Add (new string[] {year.ToString(), month.ToString(), day.ToString(), distance.ToString()});	
		}
		WriteCSV (year, month, day, float.Parse(dataList[index][3]));
	}

	public float LoadDistance (int year, int month, int day) {
		for (int i = 0; i < dataList.Count; i++) {
			if (int.Parse(dataList[i][0]) == year && int.Parse(dataList[i][1]) == month && int.Parse(dataList[i][2]) == day) {
				return float.Parse (dataList [i][3]);
			}
		}
		return 0;
	}

	public float TotalDistance () {
		float total = 0;
		for (int i = 0; i < dataList.Count; i++) {
			total += float.Parse (dataList [i] [3]);
		}
		return total;
	}

	public float TotalAvg () {
		if (dataList.Count == 0) {
			return 0;
		}
		return TotalDistance() / dataList.Count;
	}

	public float WeekDistance () {
		float total = 0;
		int s = 0;
		if (dataList.Count > 7) {
			s = dataList.Count - 7;
		}
		for (int i = s; i < dataList.Count; i++) {
			total += float.Parse (dataList [i] [3]);
		}
		return total;
	}

	public float WeekAvg () {
		if (dataList.Count == 0) {
			return 0;
		} else if (dataList.Count < 7) {
			return WeekDistance () / dataList.Count;
		}
		return WeekDistance () / 7;
	}

	public float GetSize () {
		return 0.5f + 1.5f / (WeekAvg() + 1f);
	}

	// CSVにかく
	private void WriteCSV (int year, int month, int day, float distance) {
		List<string> lineList = new List<string>();
		bool flag = false;
		for (int i = 0; i < dataList.Count; i++) {
			if (int.Parse(dataList[i][0]) == year && int.Parse(dataList[i][1]) == month && int.Parse(dataList[i][2]) == day) {
				dataList[i][3] = float.Parse(dataList[i][3]).ToString();
				flag = true;
			}
			lineList.Add(dataList[i][0] + "," + dataList[i][1] + "," + dataList[i][2] + "," + dataList[i][3]);
		}
		if (flag) {
			string[] lines = new string[lineList.Count];
			for (int i = 0; i < lines.Length; i++) {
				lines [i] = lineList [i];
			}
			System.IO.File.WriteAllLines(filePath, lines);
			return;
		}

		StreamWriter sw;
		FileInfo fi;
		fi = new FileInfo (Application.dataPath + "/Resources/SaveData.csv");
		sw = fi.AppendText ();
		sw.WriteLine (year.ToString() + "," + month.ToString() + "," + day.ToString() +"," + distance.ToString());

		// 終了
		sw.Flush ();
		sw.Close ();
	}

	private void ReadCSV () {
		TextAsset ta = Resources.Load ("SaveData") as TextAsset;
		StringReader sr = new StringReader (ta.text);
		// 1行ずつ最後まで読む
		while (sr.Peek() >= 0) {
			string line = sr.ReadLine ();
			string[] array = line.Split (',');
			dataList.Add (array);
		}
	}
}
