using UnityEngine;
using System.Collections;

public class BattleStartPanel : UIPanelBase
{
	public override void OnOpen(params object[] args)
	{
	}
	public override void OnClose()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnStartClick()
	{
		UIManager.instance.ClosePanel(this);
		Game.instance.LoadBattle();
	}

	public void OnCharacterInfoBtnClick()
	{
		UIManager.instance.OpanelPanel(UIPanelDefineType.CharacterInfoPanel);
	}
}
