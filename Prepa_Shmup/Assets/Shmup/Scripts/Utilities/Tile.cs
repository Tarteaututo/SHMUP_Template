using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField]
	private bool _hideGraphicsWhenUnactive = true;

	[SerializeField]
	private bool _useOnce = false;

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
	private bool _scaleAtActivation = false;

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

	[System.NonSerialized]
	private Coroutine _activationCoroutine = null;

	public delegate void TileEvent(Tile sender, Vector3 endPosition);
	public event TileEvent LengthExceeded = null;

	public float ExitLength => _endPosition.localPosition.z + _exitLength;

	public void Activate(bool isActive, Vector3 worldPosition)
	{
		_eventThrown = false;
		transform.position = worldPosition;
		//transform.localPosition = Vector3.zero;

		if (_hideGraphicsWhenUnactive == true)
		{
			gameObject.SetActive(isActive);
		}
		else
		{
			enabled = isActive;
		}

		if (_scaleAtActivation == true)
		{
			transform.localScale = Vector3.zero;
			if (_activationCoroutine != null)
			{
				StopCoroutine(_activationCoroutine);
				_activationCoroutine = null;
			}
			_activationCoroutine = GameManager.Instance.StartCoroutine(OnActivate(isActive));
		}
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
			if (_useOnce == true)
			{
				Destroy(gameObject);
			}
			else
			{
				Activate(false, Vector3.zero);
			}
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
			yield return null;
		}
		_activationCoroutine = null;
	}
}
