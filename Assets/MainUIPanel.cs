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
	private List<CharacterMoveBarItem> items = new List<CharacterMoveBarItem>();

	public override void OnOpen(params object[] args)
	{
		itemPrefab.gameObject.SetActive(false);
		EventManager.instance.SubscribeEvent((int)EventType.RoundStart, OnRoundStart);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterBorn, OnCharacterBorn);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
	}
	public override void OnClose()
	{
		EventManager.instance.UnsubscribeEvent((int)EventType.RoundStart, OnRoundStart);
		EventManager.instance.UnsubscribeEvent((int)EventType.CharacterBorn, OnCharacterBorn);
		EventManager.instance.UnsubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
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

	public void OnRoundStart(Event e)
	{
		roundLabel.gameObject.SetActive(true);
		roundLabel.text = BattleManager.instance.CurRound.ToString();
		var tween = roundLabel.GetComponent<UIPlayTween>();
		tween.Play(true);
		Invoke("HideRoundLabel", 2);
	}

	public void HideRoundLabel()
	{
		roundLabel.gameObject.SetActive(false);
	}
}
