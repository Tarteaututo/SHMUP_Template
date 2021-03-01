namespace Gameleon.Shmup
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class GenericMBSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		#region Fields
		protected static T _instance = null;
		#endregion Fields

		#region Properties
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = CheckForMultipleInstances();
				}
				return _instance;
			}
		}

		public static bool HasInstance
		{
			get { return _instance != null; }
		}
		#endregion Properties

		#region Methods
		protected virtual void Awake()
		{
			_instance = CheckForMultipleInstances();
		}

		protected virtual void OnDestroy()
		{
			_instance = null;
		}
		
		protected static T CheckForMultipleInstances()
		{
			T[] instances = FindObjectsOfType<T>();

			if (instances == null || instances.Length < 1)
			{
				throw new System.Exception(string.Format(" There is no instance of {0}.", typeof(T)));
			}

			if (instances.Length > 1)
			{
				throw new System.Exception(string.Format(" There is more than one instance of {0}.", typeof(T)));
			}
			return instances[0];
		}
		#endregion Methods
	}
}