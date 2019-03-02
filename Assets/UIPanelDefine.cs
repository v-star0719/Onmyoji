using System.Collections.Generic;

public enum UIPanelDefineType
{
	MainUIPanel,
	BattleStartPanel,
	BattleResultPanel,
	DamageNumPanel,
	CharacterTitlePanel,
	CharacterInfoPanel,
}


public static class UIPanelDefine
{
	public static Dictionary<int, string> panelNames = new Dictionary<int, string>();
	static UIPanelDefine()
	{
		panelNames.Add((int)UIPanelDefineType.MainUIPanel, "MainUIPanel");
		panelNames.Add((int)UIPanelDefineType.BattleStartPanel, "BattleStartPanel");
		panelNames.Add((int)UIPanelDefineType.BattleResultPanel, "BattleResultPanel");
		panelNames.Add((int)UIPanelDefineType.DamageNumPanel, "DamageNumPanel");
		panelNames.Add((int)UIPanelDefineType.CharacterTitlePanel, "CharacterTitlePanel");
		panelNames.Add((int)UIPanelDefineType.CharacterInfoPanel, "CharacterInfoPanel");
	}
}