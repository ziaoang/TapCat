using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
	void Awake()
	{
		Messenger.AddListener(GameEvent.Msg_Click, ClickHandler);
		Messenger.AddListener(GameEvent.Msg_Second, SecondHandler);
	}

	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.Msg_Click, ClickHandler);
		Messenger.RemoveListener(GameEvent.Msg_Second, SecondHandler);
	}

	void ClickHandler()
	{
		SharedData.vitality += SharedData.vitalityPerClick;
		Messenger.Broadcast (GameEvent.UI_Vitality);
	}

	void SecondHandler()
	{
		SharedData.vitality += SharedData.vitalityPerSecond;
		Messenger.Broadcast (GameEvent.UI_Vitality);
	}
}
