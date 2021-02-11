using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	[SerializeField]
	private int _collisionDamage = 1;

	[SerializeField]
	private float _moveSpeed = 1f;

	[SerializeField]
	private float _destinationThreshold = 0.1f;

	[System.NonSerialized]
	private Path _path = null;

	[System.NonSerialized]
	private int _currentPathIndex = 0;

	[System.NonSerialized]
	private bool _isReverse = false;

	private Transform _currentDestination = null;

	public int GetCollisionDamage()
	{
		return _collisionDamage;
	}

	public void SetPath(Path path, bool isReverse)
	{
		_path = path;
		_isReverse = isReverse;

		if (isReverse == true)
		{
			_currentPathIndex = path.GetDestinationCount() - 1;
		}

		transform.position = _path.GetDestination(_currentPathIndex).position;
	}

	public void SetSpeed(float speed)
	{
		_moveSpeed = speed;
	}

	private void OnEnable()
	{
		GameManager.Instance.AddActor(this);
	}

	private void OnDisable()
	{
		if (GameManager.HasInstance == true)
		{
			GameManager.Instance.RemoveActor(this);
		}
	}

	private void Update()
	{
		if (_path == null)
		{
			Debug.LogErrorFormat("{0}.Update() no path for {1}", GetType().Name, gameObject.name);
			return;
		}

		if (_currentDestination == null)
		{
			_currentDestination = _path.GetDestination(_currentPathIndex);
			UpdateToNextWaypoint();
			if (_currentDestination == null)
			{
				Destroy(gameObject);
			}
		}

		if (_currentDestination != null)
		{
			MoveToDestination(_currentDestination.position);
		}
	}

	private void MoveToDestination(Vector3 position)
	{
		Vector3 direction = (position - transform.position).normalized;
		transform.position = transform.position + direction * _moveSpeed * Time.deltaTime;

		if (direction != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
		}

		if (Vector3.Distance(transform.position, _currentDestination.position) < _destinationThreshold)
		{
			_currentDestination = null;
		}
	}

	private void UpdateToNextWaypoint()
	{
		if (_isReverse == true)
		{
			_currentPathIndex--;
		}
		else
		{
			_currentPathIndex++;
		}
	}
}
