using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TriggerEvent
{
	Enter,
	Stay,
	Exit
}

public abstract class ATriggerDispatcher : MonoBehaviour
{
	public class TriggerDispatcherEventArgs : System.EventArgs
	{
		public Collider other;
		TriggerEvent triggerEvent;

		public TriggerDispatcherEventArgs(Collider other, TriggerEvent triggerEvent)
		{
			this.other = other;
			this.triggerEvent = triggerEvent;
		}
	}

	[System.Serializable]
	public class TriggerDispatcher_UnityEvent : UnityEvent<ATriggerDispatcher, TriggerDispatcherEventArgs> { }

}