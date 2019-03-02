using System.Collections;
using System.Collections.Generic;
using Kernel;
using UnityEngine;
using Event = Kernel.Event;

public class CharacterMoveBarData
{
	public Characer characer;
	public float pos;

	public bool IsArrived
	{
		get { return Mathf.Abs(BattleProgress.TRACK_LENGTH - pos) < 0.0001f; }
	}
}

public class BattleProgress : MonoBehaviour
{
	public const float TRACK_LENGTH = 100;
	private List<CharacterMoveBarData> moveBarDatas = new List<CharacterMoveBarData>();

	public static BattleProgress instance;

	public CharacterMoveBarData arrivedCharacerData;

	public Characer ArrivedCharacer
	{
		get { return arrivedCharacerData != null ? arrivedCharacerData.characer : null; }
	}

	void Awake()
	{
		instance = this;
		EventManager.instance.SubscribeEvent((int)EventType.CharacterBorn, OnCharacterBorn);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
	}

	// Use this for initialization
	void Start () {
		
	}

	void OnDestory()
	{
		EventManager.instance.UnsubscribeEvent((int)EventType.CharacterBorn, OnCharacterBorn);
		EventManager.instance.UnsubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void BattleStart()
	{
		moveBarDatas.Clear();
	}

	public void BattleEnd()
	{
	}

	public void Reset()
	{
		foreach (var d in moveBarDatas)
		{
			d.pos = 0;
		}
	}

	public void RoundStart()
	{
		arrivedCharacerData = null;
		foreach(var d in moveBarDatas)
		{
			if(d.IsArrived)
			{
				arrivedCharacerData = d;
			}
		}
	}

	public void RoundEnd()
	{
		arrivedCharacerData.pos -= TRACK_LENGTH;
	}

	public void RunOnce()
	{
		//找到最先走到头的那个，计算他到达终点的时间，其他的按照这时间走一定的距离，都是匀速运动
		float firstTime = 1000000;
		foreach (var d in moveBarDatas)
		{
			float t = (TRACK_LENGTH - d.pos) / d.characer.speed;
			if (t < firstTime)
			{
				firstTime = t;
			}
		}

		//按照最先走到的那个人时间走
		foreach(var d in moveBarDatas)
		{
			d.pos += firstTime * d.characer.speed;
		}
	}
	
	public CharacterMoveBarData GetCharacerData(Characer c)
	{
		return moveBarDatas.Find((d) => d.characer == c);
	}

	public void PushDownCharacterPos(Characer c, float percent)
	{
		var d = instance.GetCharacerData(c);
		d.pos -= TRACK_LENGTH * percent;
		if(d.pos < 0)
		{
			d.pos = 0;
		}
	}

	public void PullUpCharacterPos(Characer c, float percent)
	{
		var d = instance.GetCharacerData(c);
		d.pos += TRACK_LENGTH * percent;
		if(d.pos > TRACK_LENGTH)
		{
			d.pos = TRACK_LENGTH;
		}
	}

	public void OnCharacterBorn(Event e)
	{
		CharacterBornEvent ee = e as CharacterBornEvent;
		CharacterMoveBarData d = new CharacterMoveBarData();
		d.characer = ee.characer;
		d.pos = 0;
		moveBarDatas.Add(d);
	}


	public void OnCharacterDead(Event e)
	{
		CharacterDeadEvent ee = e as CharacterDeadEvent;
		foreach (var d in moveBarDatas)
		{
			if (d.characer == ee.characer)
			{
				moveBarDatas.Remove(d);
				return;
			}
		}
	}
}
