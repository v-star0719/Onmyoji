using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kernel;
using Event = Kernel.Event;

public class DamageNumPanel : UIPanelBase
{
	public DamageNumItem normalItemPrefab;
	public DamageNumItem critItemPrefab;
	public DamageNumItem healItemPrefab;
	private List<DamageNumItem> items = new List<DamageNumItem>();

	public override void OnOpen(params object[] args)
	{
		normalItemPrefab.gameObject.SetActive(false);
		critItemPrefab.gameObject.SetActive(false);
		healItemPrefab.gameObject.SetActive(false);
		EventManager.instance.SubscribeEvent((int)EventType.OnDamage, OnDamage);
		EventManager.instance.SubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
	}
	public override void OnClose()
	{
		EventManager.instance.UnsubscribeEvent((int)EventType.OnDamage, OnDamage);
		EventManager.instance.UnsubscribeEvent((int)EventType.CharacterDead, OnCharacterDead);
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < items.Count;)
		{
			if (items[i].isFinished)
			{
				GameObject.Destroy(items[i].gameObject);
				items.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	public void OnDamage(Event e)
	{
		DamageEvent ee = e as DamageEvent;

		DamageNumItem itemPrefab = null;
		if (ee.damageInfo.isHeal)
		{
			itemPrefab = healItemPrefab;
		}
		else if(ee.damageInfo.isCrit)
		{
			itemPrefab = critItemPrefab;
		}
		else
		{
			itemPrefab = normalItemPrefab;
		}

		var item = Instantiate(itemPrefab);
		item.transform.parent = itemPrefab.transform.parent;
		item.transform.localScale = Vector3.one;
		item.gameObject.SetActive(true);
		item.SetData(ee.receiver, (int)ee.damageInfo.damage);
		items.Add(item);
	}

	public void OnCharacterDead(Event e)
	{
		CharacterDeadEvent ee = e as CharacterDeadEvent;
		var item = items.Find(d => d.characer == ee.characer);
		if(item != null)
		{
			items.Remove(item);
			GameObject.Destroy(item.gameObject);
		}
	}
}
