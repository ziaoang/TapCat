// Usage example:
// Messenger<float>.AddListener(0x0001, MyEventHandler);
// Messenger<float>.Broadcast(0x0001, 1.0f);
// Messenger<float>.RemoveListener(0x0001, MyEventHandler);

using System;
using System.Collections.Generic;
using System.Linq;

static internal class NotificationCenter
{
	static public Dictionary<ushort, Delegate> eventTable = new Dictionary<ushort, Delegate>();

	static public void AddListener(ushort eventType, Delegate handler)
	{
		NotificationCenter.OnListenerAdding(eventType, handler);
		eventTable[eventType] = Delegate.Combine(eventTable[eventType], handler);
	}

	static public void RemoveListener(ushort eventType, Delegate handler)
	{
		NotificationCenter.OnListenerRemoving(eventType, handler);
		eventTable[eventType] = Delegate.Remove(eventTable[eventType], handler);
		NotificationCenter.OnListenerRemoved(eventType);
	}

	static public T[] GetInvocationList<T>(ushort eventType)
	{
		if (eventTable.ContainsKey (eventType))
		{
			Delegate d = eventTable[eventType];
			if (d != null)
			{
				return d.GetInvocationList().Cast<T>().ToArray();
			}
			else
			{
				throw new BroadcastException(string.Format("Broadcasting message 0x{0:x} but no listener found.", eventType));
			}
		}
		return null;
	}

	static public void OnListenerAdding(ushort eventType, Delegate handler)
	{
		if (!eventTable.ContainsKey(eventType))
		{
			eventTable.Add(eventType, null);
		}

		var d = eventTable[eventType];
		if (d != null && d.GetType() != handler.GetType())
		{
			throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type 0x{0:x}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, handler.GetType().Name));
		}
	}

	static public void OnListenerRemoving(ushort eventType, Delegate handler)
	{
		if (eventTable.ContainsKey(eventType))
		{
			var d = eventTable[eventType];
			if (d == null)
			{
				throw new ListenerException(string.Format("Attempting to remove listener with for event type 0x{0:x} but current listener is null.", eventType));
			}
			else if (d.GetType() != handler.GetType())
			{
				throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type 0x{0:x}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, handler.GetType().Name));
			}
		}
		else
		{
			throw new ListenerException(string.Format("Attempting to remove listener for type 0x{0:x} but Messenger doesn't know about this event type.", eventType));
		}
	}

	static public void OnListenerRemoved(ushort eventType)
	{
		if (eventTable[eventType] == null)
		{
			eventTable.Remove(eventType);
		}
	}

	static public void OnBroadcasting(ushort eventType)
	{
		if (!eventTable.ContainsKey(eventType))
		{
			throw new NotificationCenter.BroadcastException(string.Format("Broadcasting message 0x{0:x} but no listener found.", eventType));
		}
	}
	
	public class ListenerException : Exception
	{
		public ListenerException(string msg):base(msg){}
	}

	public class BroadcastException : Exception
	{
		public BroadcastException(string msg):base(msg){}
	}
}

// No parameters
static public class Messenger
{
	static public void AddListener(ushort eventType, Action handler)
	{
		NotificationCenter.AddListener(eventType, handler);
	}

	static public void RemoveListener(ushort eventType, Action handler)
	{
		NotificationCenter.RemoveListener(eventType, handler);
	}

	static public void Broadcast(ushort eventType)
	{
		NotificationCenter.OnBroadcasting(eventType);
		var invocationList = NotificationCenter.GetInvocationList<Action>(eventType);
		foreach (var callback in invocationList) 
		{
			callback.Invoke ();
		}
	}
}

// One parameter
static public class Messenger<T>
{
	static public void AddListener(ushort eventType, Action<T> handler)
	{
		NotificationCenter.AddListener(eventType, handler);
	}

	static public void RemoveListener(ushort eventType, Action<T> handler)
	{
		NotificationCenter.RemoveListener(eventType, handler);
	}

	static public void Broadcast(ushort eventType, T arg1)
	{
		NotificationCenter.OnBroadcasting(eventType);
		var invocationList = NotificationCenter.GetInvocationList<Action<T>>(eventType);
		foreach (var callback in invocationList) 
		{
			callback.Invoke (arg1);
		}
	}
}


// Two parameters
static public class Messenger<T, U>
{
	static public void AddListener(ushort eventType, Action<T, U> handler)
	{
		NotificationCenter.AddListener(eventType, handler);
	}

	static public void RemoveListener(ushort eventType, Action<T, U> handler)
	{
		NotificationCenter.RemoveListener(eventType, handler);
	}

	static public void Broadcast(ushort eventType, T arg1, U arg2)
	{
		NotificationCenter.OnBroadcasting(eventType);
		var invocationList = NotificationCenter.GetInvocationList<Action<T, U>>(eventType);
		foreach (var callback in invocationList)
		{
			callback.Invoke (arg1, arg2);
		}
	}
}


// Three parameters
static public class Messenger<T, U, V>
{
	static public void AddListener(ushort eventType, Action<T, U, V> handler)
	{
		NotificationCenter.AddListener(eventType, handler);
	}

	static public void RemoveListener(ushort eventType, Action<T, U, V> handler)
	{
		NotificationCenter.RemoveListener(eventType, handler);
	}

	static public void Broadcast(ushort eventType, T arg1, U arg2, V arg3)
	{
		NotificationCenter.OnBroadcasting(eventType);
		var invocationList = NotificationCenter.GetInvocationList<Action<T, U, V>>(eventType);
		foreach (var callback in invocationList)
		{
			callback.Invoke (arg1, arg2, arg3);
		}
	}
}


