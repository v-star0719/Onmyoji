using UnityEngine;
using System.Collections;
using System.Text;
using Kernel;
using Event = Kernel.Event;

public class CharacterTitleItem : MonoBehaviour
{
	public Characer characer;
	private Vector3 offset;

	public UISprite hpSprite;
	public UILabel nameLabel;
	public UILabel buffLabel;

	public void SetData(Characer c)
	{
		characer = c;
		offset = new Vector3(0, 2, 0);
		nameLabel.text = c.Name;
	}

	// Use this for initialization
	void Start()
	{
		EventManager.instance.SubscribeEvent((int)EventType.AddBuff, OnAddBuff);
		EventManager.instance.SubscribeEvent((int)EventType.RemoveBuff, OnRemoveBuff);
	}

	void OnDestory()
	{
		EventManager.instance.UnsubscribeEvent((int)EventType.AddBuff, OnAddBuff);
		EventManager.instance.UnsubscribeEvent((int)EventType.RemoveBuff, OnAddBuff);
	}

	// Update is called once per frame
	void Update()
	{
		var pos = Camera.main.WorldToScreenPoint(characer.transform.position + offset);
		pos = UICamera.list[0].cachedCamera.ScreenToWorldPoint(pos);
		pos.z = 0;
		transform.position = pos;
		hpSprite.fillAmount = characer.GetHp(true);
	}

	public void OnAddBuff(Event e)
	{
		AddBuffEvent ee = e as AddBuffEvent;
		OnBuffChanged(ee.characer);
	}

	public void OnRemoveBuff(Event e)
	{
		RemoveBuffEvent ee = e as RemoveBuffEvent;
		OnBuffChanged(ee.characer);
	}

	private void OnBuffChanged(Characer c)
	{
		StringBuilder sb = new StringBuilder("");
		for (var i = 0; i < c.buffs.Count; i++)
		{
			var buff = c.buffs[i];
			sb.Append(buff.config.name);
			if (i < c.buffs.Count - 1)
			{
				sb.Append("\n");
			}
		}
		buffLabel.text = sb.ToString();
		buffLabel.gameObject.SetActive(string.IsNullOrEmpty(buffLabel.text));
	}
}
