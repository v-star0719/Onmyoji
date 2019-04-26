using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Kernel;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Event = Kernel.Event;

public class MainUIPanel : UIPanelBase
{
	public CharacterMoveBarItem itemPrefab;
	public UILabel roundLabel;
	public UILabel fireCounLabel;
	public UILabel fireRecoverLabel;
	public GameObject[] roundTimeMarks;
	private List<CharacterMoveBarItem> items = new List<CharacterMoveBarItem>();

	public override void OnOpen(params object[] args)
	{
		itemPrefab.gameObject.SetActive(false);
		EventManager.instance.SubscribeEvent((int)EventType.WaveStart, OnWaveStart);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterBorn, OnCharacterBorn);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
		EventManager.instance.SubscribeEvent((int)EventType.ChangeFire, OnChangeFire);
		EventManager.instance.SubscribeEvent((int)EventType.RoundStart, OnRoundStart);
	}
	public override void OnClose()
	{
		EventManager.instance.UnsubscribeEvent((int)EventType.WaveStart, OnWaveStart);
		EventManager.instance.UnsubscribeEvent((int)EventType.CharacterBorn, OnCharacterBorn);
		EventManager.instance.UnsubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
		EventManager.instance.UnsubscribeEvent((int)EventType.ChangeFire, OnChangeFire);
		EventManager.instance.UnsubscribeEvent((int)EventType.RoundStart, OnRoundStart);
	}

	public void OnCharacterBorn(Event e)
	{
		CharacterBornEvent ee = e as CharacterBornEvent;
		var item = Instantiate(itemPrefab);
		item.transform.parent = itemPrefab.transform.parent;
		item.transform.localScale = Vector3.one;
		item.gameObject.SetActive(true);
		item.SetData(BattleProgress.instance.GetCharacerData(ee.characer));
		items.Add(item);
	}

	public void OnCharacterDead(Event e)
	{
		CharacterDeadEvent ee = e as CharacterDeadEvent;
		var item = items.Find(d => d.data.characer == ee.characer);
		if (item != null)
		{
			items.Remove(item);
			GameObject.Destroy(item.gameObject);
		}
	}

	public void OnWaveStart(Event e)
	{
		string[] texts =
		{
			"一回合",
			"二回合",
			"三回合",
		};
		roundLabel.gameObject.SetActive(true);
		roundLabel.text = texts[BattleManager.instance.CurRound];
		var tween = roundLabel.GetComponent<UIPlayTween>();
		tween.Play(true);
		Invoke("HideRoundLabel", 2);
	}

	public void OnChangeFire(Event e)
	{
		fireCounLabel.text = BattleManager.instance.FireCount.ToString();
	}

	public void OnRoundStart(Event e)
	{
		fireRecoverLabel.text = "+" + BattleManager.instance.FireRecovery.ToString();
		int n = BattleManager.instance.UserRoundTimes;
		for(int i = 0; i < 5; i++)
		{
			roundTimeMarks[i].SetActive(i < n -1);
		}
	}

	public void HideRoundLabel()
	{
		roundLabel.gameObject.SetActive(false);
	}
}
