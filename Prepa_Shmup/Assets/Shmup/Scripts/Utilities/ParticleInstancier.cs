using System.Collections;
using UnityEngine;

public class ParticleInstancier : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem _particleSystem = null;

	[SerializeField]
	private bool _copyInstancierRotation = false;

	public void DoInstantiate()
	{
		var instance = Instantiate(_particleSystem);
		instance.transform.position = transform.position;

		if (_copyInstancierRotation == true)
		{
			instance.transform.rotation = transform.rotation;
		}
	}
}
