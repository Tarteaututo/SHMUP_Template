using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private string _inputName = "Jump";

	[SerializeField]
	private float _fireRate = 0.1f;

	[System.NonSerialized]
	private float _currentFireTime = 0;
	#endregion Fields

	#region Properties
	/// <summary>
	/// Remaining time before the bomb can be used again.
	/// </summary>
	public float CurrentFireTime => _currentFireTime;
	#endregion Properties

	#region Methods
	private void Update()
	{
		UpdateInputs();
	}

	private void UpdateInputs()
	{
		bool fired = Input.GetButton(_inputName);

		if (fired == true && _currentFireTime <= 0)
		{
			_currentFireTime = _fireRate;
			DoBomb();
		}

		if (_currentFireTime > 0)
		{
			_currentFireTime -= Time.deltaTime;
		}
	}

	private void DoBomb()
	{
		GameManager.Instance.DestroyAllActorsAndProjectiles();
	}
	#endregion Methods
}
