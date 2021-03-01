using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class PoolTester : MonoBehaviour
{
	[SerializeField]
	private PoolItem _poolItem = null;

	[SerializeField]
	private bool _autoAddItem = false;

	private void Start()
	{
		if (_autoAddItem == true)
		{
			AddItem();
		}
	}

	private void OnDestroy()
	{
		if (_autoAddItem == true)
		{
			if (PoolManager.HasInstance == true)
			{
				RemoveItem();
			}
		}
	}

	[ResponsiveButtonGroup("LifeCycle")]
	private void AddItem()
	{
		PoolManager.Instance.AddItem(_poolItem, 3);
	}

	[ResponsiveButtonGroup("LifeCycle")]
	private void RemoveItem()
	{
		PoolManager.Instance.RemoveItem(_poolItem);
	}

	[Button]
	private PoolItem InstantiateItem()
	{
		var instance = PoolManager.Instance.Get(_poolItem);
		instance.transform.position = transform.position + Random.insideUnitSphere * 3;
		return instance;
	}
}
