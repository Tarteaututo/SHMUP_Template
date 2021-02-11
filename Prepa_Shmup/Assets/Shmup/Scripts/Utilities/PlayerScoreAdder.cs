using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreAdder : MonoBehaviour
{
	public void AddToScore()
	{
		GameManager.Instance.AddScore();
	}

	public void AddToScore(Damageable damageable)
	{
		GameManager.Instance.AddScore(damageable.GetComponent<ScoreGiver>());
	}
}
