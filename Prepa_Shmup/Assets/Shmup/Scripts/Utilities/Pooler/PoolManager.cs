using Gameleon.Shmup;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : GenericMBSingleton<PoolManager>
{
	#region Fields
	[SerializeField]
	private List<PoolItem> _poolItemsConfiguration = null;

	[System.NonSerialized]
	private Dictionary<PoolItem, Queue<PoolItem>> _runtimePoolItems = null;
	#endregion Fields


	// TODO AL : do preconfiguration by editor with runtime fill from config
	#region Methods
	protected override void Awake()
	{
		base.Awake();
		_runtimePoolItems = new Dictionary<PoolItem, Queue<PoolItem>>();
	}

	/// <summary>
	/// Add a new PoolItem to the Pool list.
	/// </summary>
	/// <param name="item">The item to add. It will be ignored if it is already in the list.</param>
	/// <param name="preWarmCount">Count of item to pre instantiate</param>
	public void AddItem(PoolItem item, int preWarmCount)
	{
		if (_poolItemsConfiguration.Contains(item) == false)
		{
			_poolItemsConfiguration.Add(item);
			Queue<PoolItem> preWarmList = new Queue<PoolItem>(preWarmCount);
			_runtimePoolItems.Add(item, preWarmList);
			for (int i = 0; i < preWarmCount; i++)
			{
				preWarmList.Enqueue(InstantiateItem(item, false));
			}
		}
	}

	/// <summary>
	/// Remove a PoolItem from the Pool list.
	/// </summary>
	/// <param name="item">The item to remove.</param>
	public void RemoveItem(PoolItem item)
	{
		if (_poolItemsConfiguration.Remove(item) == true)
		{
			if (_runtimePoolItems.TryGetValue(item, out Queue<PoolItem> poolItemInstances) == true)
			{
				for (int i = 0, length = poolItemInstances.Count; i < length; i++)
				{
					DestroyItem(poolItemInstances.Dequeue());
				}
				_runtimePoolItems.Remove(item);
			}
			else
			{
				// log error
			}
		}
	}

	/// <summary>
	/// Get a new instance of the item.
	/// Lookup the list of PoolItem and and get the first activeSelf item found.
	/// If there are no disabled item, instantiate a new one.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="count"></param>
	/// <returns></returns>
	public PoolItem Get(PoolItem item, int count = 1)
	{
		if (item == null)
		{
			Debug.LogErrorFormat("{0}.Get(null) Trying to get a null PoolItem ", GetType().Name);
			return null;
		}
		bool result = _runtimePoolItems.TryGetValue(item, out Queue<PoolItem> poolItemInstances);

		if (result == false)
		{
			AddItem(item, count);
			_runtimePoolItems.TryGetValue(item, out poolItemInstances);
		}
		if (poolItemInstances.Count == 0)
		{
			return InstantiateItem(item);
		}
		else
		{
			var instance = poolItemInstances.Dequeue();
			instance.gameObject.SetActive(true);
			return instance;
		}
	}

	// TODO AL : rename reference and find a clever name
	/// <summary>
	/// Disable a PoolItem instance and move it back to the pool list.
	/// </summary>
	/// <param name="referenceItem">The prefab reference of the item. NOT THE INSTANCE REFERENCE></param>
	public bool ReturnToPool(PoolItem referenceItem, PoolItem item)
	{
		if (referenceItem == null)
		{
			Debug.LogErrorFormat("{0}.ReturnToPool(null) Trying to return a null reference item", GetType().Name);
			return false;
		}
		if (_runtimePoolItems.TryGetValue(referenceItem, out Queue<PoolItem> value) == true)
		{
			item.gameObject.SetActive(false);
			value.Enqueue(item);
			return true;
		}

		Debug.LogErrorFormat("{0}.ReturnToPool(null) Trying to return to pool an object that is doesn't present in runtime pool items.", GetType().Name);
		return false;
	}

	private PoolItem InstantiateItem(PoolItem from, bool setActive = true)
	{
		var instance = Instantiate(from);
		instance.SetPoolReference(from);
		instance.gameObject.SetActive(setActive);
		return instance;
	}

	private void DestroyItem(PoolItem from)
	{
		Destroy(from);
	}
	#endregion Methods
}
