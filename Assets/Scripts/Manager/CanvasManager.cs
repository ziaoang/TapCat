using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;

	void Awake()
	{
		Messenger.AddListener(GameEvent.UI_Vitality, OnVitalityUpdate);
		Messenger.AddListener(GameEvent.UI_ClickCnt, OnClickCntUpdate);
	}

	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.UI_Vitality, OnVitalityUpdate);
		Messenger.RemoveListener(GameEvent.UI_ClickCnt, OnClickCntUpdate);
	}

	void Start()
	{
		text1.text = "生命力: " + SharedData.vitality;
		text2.text = "每次点击生命力: " + SharedData.vitalityPerClick;
		text3.text = "每秒回复生命力: " + SharedData.vitalityPerSecond;
//		text4.text = "点击数: " + SharedData.clickCnt;
		text4.text = "点击数: " + new BigInt().ToString();
	}

	void OnVitalityUpdate()
	{
		text1.text = "生命力: " + SharedData.vitality;
	}

	void OnClickCntUpdate()
	{
		text4.text = "点击数: " + SharedData.clickCnt;
	}
}
