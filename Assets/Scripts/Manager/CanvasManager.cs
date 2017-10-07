using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public Text text1;
	public Text text2;
	public Text text3;

	void Awake()
	{
		Messenger.AddListener(GameEvent.UI_Vitality, OnVitalityUpdate);
	}

	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.UI_Vitality, OnVitalityUpdate);
	}

	void Start()
	{
		text1.text = "生命力: " + SharedData.vitality;
		text2.text = "每次点击生命力: " + SharedData.vitalityPerClick;
		text3.text = "每秒回复生命力: " + SharedData.vitalityPerSecond;
	}

	void OnVitalityUpdate()
	{
		text1.text = "生命力: " + SharedData.vitality;
	}
}
