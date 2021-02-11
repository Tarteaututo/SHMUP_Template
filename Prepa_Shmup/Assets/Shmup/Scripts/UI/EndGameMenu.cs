using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _victoryText = null;

	[SerializeField]
	private TextMeshProUGUI _defeatText = null;

	[SerializeField]
	private TextMeshProUGUI _killCountText = null;

	public void DoEndGame(bool isWin)
	{
		gameObject.SetActive(true);

		if (isWin == true)
		{
			_victoryText.gameObject.SetActive(true);
		}
		else
		{
			_defeatText.gameObject.SetActive(true);
		}
	}

	public void UpdateMenu(int killCount)
	{
		_killCountText.text = killCount.ToString();
	}
}
