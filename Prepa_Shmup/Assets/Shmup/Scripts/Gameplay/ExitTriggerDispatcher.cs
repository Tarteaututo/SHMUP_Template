using UnityEngine;

public class ExitTriggerDispatcher : ATriggerDispatcher
{
	public TriggerDispatcher_UnityEvent TriggerExit = null;

	private void OnTriggerExit(Collider other)
	{
		if (TriggerExit != null)
		{
			TriggerExit.Invoke(this, new TriggerDispatcherEventArgs(other, TriggerEvent.Exit));
		}
	}
}

