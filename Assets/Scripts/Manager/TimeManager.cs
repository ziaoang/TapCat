using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	float time = 0;

	void Update () 
	{
		time += Time.deltaTime;
		if (time >= 1) 
		{
			time -= 1;
			Messenger.Broadcast (GameEvent.Msg_Second);
		}
	}
}
