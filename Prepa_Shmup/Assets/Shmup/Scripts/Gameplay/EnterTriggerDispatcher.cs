using UnityEngine;

public class EnterTriggerDispatcher : ATriggerDispatcher
{
	public TriggerDispatcher_UnityEvent TriggerEntered = null;

	private void OnTriggerEnter(Collider other)
	{
		if (TriggerEntered != null)
		{
			TriggerEntered.Invoke(this, new TriggerDispatcherEventArgs(other, TriggerEvent.Enter));
		}
	}


}

