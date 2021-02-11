using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
	[SerializeField]
	private bool _alwaysDrawGizmos = false;

	[SerializeField]
	private List<Transform> _path = null;

	public int GetDestinationCount()
	{
		return _path.Count;
	}

	public Transform GetDestination(int index)
	{
		if (index < 0 || index >= _path.Count)
		{
			return null;
		}

		return _path[index];
	}

	private void OnDrawGizmos()
	{
		if (_alwaysDrawGizmos == true)
		{
			if (_path == null || _path.Count == 0)
			{
				return;
			}

			for (int i = 0, length = _path.Count - 1; i < length; i++)
			{
				Gizmos.DrawLine(_path[i].position, _path[i + 1].position);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (_alwaysDrawGizmos == false)
		{
			if (_path == null || _path.Count == 0)
			{
				return;
			}

			for (int i = 0, length = _path.Count - 1; i < length; i++)
			{
				Gizmos.DrawLine(_path[i].position, _path[i + 1].position);
			}
		}
	}
}
