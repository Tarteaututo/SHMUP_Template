using UnityEngine;

public class StayTriggerDispatcher : ATriggerDispatcher
{
	public TriggerDispatcher_UnityEvent TriggerStayed = null;

	private void OnTriggerStay(Collider other)
	{
		if (TriggerStayed != null)
		{
			TriggerStayed.Invoke(this, new TriggerDispatcherEventArgs(other, TriggerEvent.Stay));
		}
	}

}

