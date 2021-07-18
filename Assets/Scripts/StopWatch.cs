using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
	public float timeStart;
	public TextMeshProUGUI textMesh;

	internal bool timerActive = true;

	void Start()
	{
		textMesh.text = timeStart.ToString("F2");
	}

	// Update is called once per frame
	void Update()
	{
		if (timerActive)
		{
			timeStart += Time.deltaTime;
			float minutes = Mathf.FloorToInt(timeStart / 60);
			float seconds = Mathf.FloorToInt(timeStart % 60);
			float milliSeconds = (timeStart % 1) * 1000;
			textMesh.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
		}
	}
}
