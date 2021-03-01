using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
	[SerializeField]
	private int _preWarmCount = 2;

	[System.NonSerialized]
	private PoolItem _poolReference = null;

	public int PreWarmCount => _preWarmCount;

	public void SetPoolReference(PoolItem poolReference)
	{
		_poolReference = poolReference;
	}

	[Button]
	public void ReturnToPool()
	{
		PoolManager.Instance.ReturnToPool(_poolReference, this);
	}
}
