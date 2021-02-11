using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerMenu : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text = null;

	[Range(1, 60)]
	[SerializeField]
	private int _updatefrequency = 10;

	public void UpdateTimer(float timeInSeconds)
	{
		if (Time.frameCount % _updatefrequency == 0)
		{
			_text.text = timeInSeconds.ToString("0.0");
		}
	}
}
