using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEmitter : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		var fireable = other.GetComponentInParent<Fireable>();
		if (fireable != null)
		{
			fireable.DoFire(transform.rotation);
		}
	}
}
