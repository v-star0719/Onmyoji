using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public static Game instance;
	public Transform uiRoot;
	public GameObject characterPrefab;
	public GameObject monsterPrefab;
	// Use this for initialization

	void Awake()
	{
		instance = this;
		UIManager.instance.Init(UIPanelDefine.panelNames, uiRoot);
	}

	void Start()
	{
		UIManager.instance.OpanelPanel(UIPanelDefineType.MainUIPanel);
		UIManager.instance.OpanelPanel(UIPanelDefineType.CharacterTitlePanel);
		UIManager.instance.OpanelPanel(UIPanelDefineType.BattleStartPanel);
		UIManager.instance.OpanelPanel(UIPanelDefineType.DamageNumPanel);
	}

	public void LoadBattle ()
	{
		var s = new int[] { 1991, 1007, 1002, 1003, 1001, 1006, };
		var monsters = new List<int[]>();
		monsters.Add(new int[] { 2001, 2002, 2003 });
		monsters.Add(new int[] { 2004, 2005, 2006 });
		monsters.Add(new int[] { 2007, 2007, 2008 });
		BattleManager.instance.StartBattle(s, monsters);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
