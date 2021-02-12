using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class Path : MonoBehaviour
{
	[SerializeField]
	private Color _gizmosColor = Color.white;

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

#if UNITY_EDITOR

	private Color GetGizmosColor()
	{
		return Selection.activeGameObject == gameObject ? Color.red : _gizmosColor;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = GetGizmosColor();
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
		Gizmos.color = GetGizmosColor();
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
#endif //UNITY_EDITOR

}
