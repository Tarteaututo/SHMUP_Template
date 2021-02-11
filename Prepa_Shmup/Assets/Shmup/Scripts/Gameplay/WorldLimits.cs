using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLimits : MonoBehaviour
{
	[SerializeField]
	private Transform _top = null;

	[SerializeField]
	private Transform _right = null;

	[SerializeField]
	private Transform _down = null;

	[SerializeField]
	private Transform _left = null;

	public Vector3 Clamp(Vector3 position)
	{
		Vector3 result = position;

		if (result.x > _right.position.x)
		{
			result.x = _right.position.x;
		}
		else if (result.x < _left.position.x)
		{
			result.x = _left.position.x;
		}

		if (result.y > _top.position.y)
		{
			result.y = _top.position.y;
		}
		else if (result.y < _down.position.y)
		{
			result.y = _down.position.y;
		}

		return result;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		Vector3 from = new Vector3(_left.position.x, _top.position.y, 0);
		Vector3 to = new Vector3(_right.position.x, _top.position.y, 0);
		Gizmos.DrawLine(from, to);

		from.y = _down.position.y;
		to.y = _down.position.y;
		Gizmos.DrawLine(from, to);

		from = new Vector3(_left.position.x, _top.position.y, 0);
		to = new Vector3(_left.position.x, _down.position.y, 0);
		Gizmos.DrawLine(from, to);

		from.x = _right.position.x;
		to.x = _right.position.x;
		Gizmos.DrawLine(from, to);
	}
}
