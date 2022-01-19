using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private WorldLimits _worldLimits = null;

	[SerializeField]
	private Damageable _damageable = null;

	[SerializeField]
	private float _moveSpeed = 1f;

	[InfoBox("List of components that will be activated with the Player.")]
	[SerializeField]
	private List<Behaviour> _components  = null;

	public void Activate(bool isActive)
	{
		enabled = isActive;
		for (int i = 0, length = _components.Count; i < length; i++)
		{
			_components[i].enabled = isActive;
		}
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
		Vector3 desiredPosition = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * _moveSpeed);
		//Vector3 desiredPosition = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * _moveSpeed);
		transform.position = _worldLimits.Clamp(desiredPosition);
	}
}
