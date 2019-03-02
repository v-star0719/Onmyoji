using UnityEngine;
using System.Collections;

public class BattleResultPanel : UIPanelBase
{
	public GameObject winObj;
	public GameObject loseObj;

	public override void OnOpen(params object[] args)
	{
		bool win = (bool)args[0];
		winObj.SetActive(win);
		loseObj.SetActive(!win);
	}
	public override void OnClose()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Show(bool win)
	{
		gameObject.SetActive(true);
		
	}

	public void OnClick()
	{
		UIManager.instance.ClosePanel(this);
		UIManager.instance.OpanelPanel(UIPanelDefineType.BattleStartPanel);
		BattleManager.instance.ClearBattle();
	}
}
