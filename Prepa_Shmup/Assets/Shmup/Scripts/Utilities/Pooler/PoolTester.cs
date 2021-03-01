using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class PoolTester : MonoBehaviour
{
	[SerializeField]
	private PoolItem _poolItem = null;

	private void Start()
	{
		AddItem();
	}

	private void OnDestroy()
	{
		if (PoolManager.HasInstance == true)
		{
			RemoveItem();
		}
	}

	//[ResponsiveButtonGroup("LifeCycle")]
	private void AddItem()
	{
		PoolManager.Instance.AddItem(_poolItem, 3);
	}

	//[ResponsiveButtonGroup("LifeCycle")]
	private void RemoveItem()
	{
		PoolManager.Instance.RemoveItem(_poolItem);
	}

	[Button]
	private PoolItem InstantiateItem()
	{
		return PoolManager.Instance.Get(_poolItem);
	}
}
