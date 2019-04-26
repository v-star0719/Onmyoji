using UnityEngine;
using System.Collections;

public class CharacterInfoPanel : UIPanelBase
{
	public UIDynamicListCtrl listCtrl;
	public UILabel nameLabel;
	public UILabel attackLabel;
	public UILabel hpLabel;
	public UILabel speedLabel;
	public UILabel critLabel;
	public UILabel critDamageLabel;

	public override void OnOpen(params object[] args)
	{
		var datas = CharacterDataManager.instance.characterDatas;
		listCtrl.SetList(datas.Count, datas, OnItemClick);
		gameObject.SetActive(true);
		OnItemClick(listCtrl.itemList[0]);
	}
	public override void OnClose()
	{
	}

	public void OnCloseClick()
	{
		UIManager.instance.ClosePanel(this);
	}

	public void OnItemClick(UIDynamicListItemCtrlBase item)
	{
		CharacterInfoItem infoItem = item as CharacterInfoItem;
		var data = infoItem.data;
		CharacterConfig conf = CharacterConfigManager.instance.Get(data.configId);
		nameLabel.text = string.Format("{0} [{1}]", data.name, data.id);
		attackLabel.text = string.Format("{0} ({1} + [04f009]{2}[-])", data.attack + conf.attack, conf.attack, data.attack);
		hpLabel.text = string.Format("{0} ({1} + [04f009]{2}[-])", data.hp + conf.hp, conf.hp, data.hp);
		speedLabel.text = string.Format("{0} ({1} + [04f009]{2}[-])", data.speed + conf.speed, conf.speed, data.speed);
		critLabel.text = string.Format("{0} ({1} + [04f009]{2}[-])", data.crit + conf.crit, conf.crit, data.crit);
		critDamageLabel.text = string.Format("{0} ({1} + [04f009]{2}[-])", data.critDamage + conf.critDamage, conf.critDamage, data.critDamage);
	}
}
