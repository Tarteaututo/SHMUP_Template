using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 1f;


	[BoxGroup("endPosition", ShowLabel = false)]
	[InfoBox("The position at which the next tile will be spawned.")]
	[SerializeField]
	private Transform _endPosition = null;

	[BoxGroup("exitLength", ShowLabel = false)]
	[InfoBox("The remaining distance needed to be traveled to get out of camera view when the endPosition reaches the top limits.")]
	[SerializeField]
	private float _exitLength = 10f;

	[SerializeField]
	private float _maxScaleDelta = 1f;

	[BoxGroup("Gizmos")]
	private bool _hideGizmos = false;

	[BoxGroup("Gizmos")]
	[SerializeField]
	private float _gizmosWidth = 10f;
	
	[BoxGroup("Gizmos")]
	[SerializeField]
	private float _gizmosThickness = 10f;

	[System.NonSerialized]
	private bool _eventThrown = false;

	public delegate void TileEvent(Tile sender, Vector3 endPosition);
	public event TileEvent LengthExceeded = null;

	public float ExitLength => _endPosition.localPosition.z + _exitLength;

	public void Activate(bool isActive, Vector3 worldPosition)
	{
		_eventThrown = false;
		transform.position = worldPosition;
		//transform.localPosition = Vector3.zero;

		transform.localScale = Vector3.zero;
		gameObject.SetActive(isActive);

		// TODO AL : cache coroutine
		GameManager.Instance.StartCoroutine(OnActivate(isActive));
	}

	private void Update()
	{
		transform.position += _moveSpeed * Time.deltaTime * -transform.forward;

		float currentZPosition = Mathf.Abs(transform.localPosition.z);
		if (_eventThrown == false && currentZPosition > _endPosition.localPosition.z)
		{
			_eventThrown = true;
			LengthExceeded?.Invoke(this, _endPosition.position);
		}
		else if (currentZPosition > ExitLength)
		{
			Activate(false, Vector3.zero);
		}
	}

	private void OnDrawGizmos()
	{
		if (_hideGizmos == true) return;

		Gizmos.color = Color.red;
		Matrix4x4 previousMatrix = Gizmos.matrix;
		Gizmos.matrix = transform.localToWorldMatrix;
		{
			Vector3 center = new Vector3(0, _gizmosThickness * 0.5f, _endPosition.localPosition.z * 0.5f);
			Vector3 size = new Vector3(_gizmosWidth, _gizmosThickness, _endPosition.localPosition.z);
			Gizmos.DrawWireCube(center, size);
		}
		Gizmos.matrix = previousMatrix;
	}


	IEnumerator OnActivate(bool isActive)
	{
		Vector3 result = isActive ? Vector3.one : Vector3.zero;

		while (transform.localScale != result)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, result, Time.deltaTime * _maxScaleDelta);
			Debug.LogError("OnActivate");
			yield return null;
		}
	}
}
