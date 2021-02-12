using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGiver : MonoBehaviour
{
	[SerializeField]
	private int _score = 1;

	public int Score => _score;

	public void AddScore()
	{
		GameManager.Instance.AddScore(this);
	}
}
