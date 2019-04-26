using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kernel;
using Event = Kernel.Event;

public class CharacterTitlePanel : UIPanelBase
{
	public CharacterTitleItem itemPrefab;
	private List<CharacterTitleItem> titleItems = new List<CharacterTitleItem>();

	public override void OnOpen(params object[] args)
	{
		itemPrefab.gameObject.SetActive(false);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterBorn, OnCharacterBorn);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
	}
	public override void OnClose()
	{
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
		item.SetData(ee.characer);
		titleItems.Add(item);
	}

	public void OnCharacterDead(Event e)
	{
		CharacterDeadEvent ee = e as CharacterDeadEvent;
		var item = titleItems.Find(d => d.characer == ee.characer);
		if(item != null)
		{
			titleItems.Remove(item);
			GameObject.Destroy(item.gameObject);
		}
	}
}
