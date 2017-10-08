using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {
	
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			SharedData.clickCnt ++;
			Messenger.Broadcast (GameEvent.UI_ClickCnt);
			Messenger.Broadcast (GameEvent.Msg_Click);
		}
	}
}
