using System.Collections;
using System.Collections.Generic;
using Kernel;
using UnityEngine;

public enum BattleState
{
	None,
	WaveStart,
	WaveRunning,
	WaveEnd,
}

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance;

	public List<Characer> characers = new List<Characer>();

	public int FireCount { get; private set;}
	public int FireRecovery { get; private set; }
	public int UserRoundTimes { get; private set; }

	private float timer;
	private bool isRunStart;
	private int[] shikigamis;
	private List<int[]> monsters;
	private int curRound = 0;
	private bool isBattleFinished;
	private bool isWaveFinish;
	private BattleState state = BattleState.None;
	private int monsterDeadCount = 0;
	private int shikigamiDeadCount = 0;

	public BattleManager()
	{
		FireRecovery = 3;
	}

	public int CurRound
	{
		get { return curRound; }
	}


	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	public void StartBattle (int[] shikigamis, List<int[]> monsters)
	{
		this.shikigamis = shikigamis;
		this.monsters = monsters;
		ChangeState(BattleState.WaveStart);
		FireCount = 3;
		FireRecovery = 3;
		curRound = 0;
		shikigamiDeadCount = 0;
		monsterDeadCount = 0;
		ChangeFire(0);
	}

	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		switch(state)
		{
			case BattleState.WaveStart:
				if (timer > 1)
				{
					timer = 0;
					state = BattleState.WaveRunning;
				}
				break;
			case BattleState.WaveRunning:

				if(timer > 1 && !isRunStart)
				{
					isRunStart = true;
					BattleProgress.instance.RunOnce();
					BattleProgress.instance.RoundStart();
					var character = BattleProgress.instance.ArrivedCharacer;
					character.DoAttack();
					if (!character.isMonster)
					{
						UserRoundTimes++;
						if (UserRoundTimes == 5)
						{
							UserRoundTimes = 0;
							ChangeFire(FireRecovery);
							if (FireRecovery < 5)
							{
								FireRecovery++;
							}
						}
					}
					EventManager.instance.FireEvent(new RoundStartEvent());
					BuffManager.instance.RoundStart();
					BuffManager.instance.Round();
				}

				if(timer > 2)
				{
					timer -= 2;
					isRunStart = false;
					BattleProgress.instance.RoundEnd();
					BuffManager.instance.RoundEnd();
					EventManager.instance.FireEvent(new RoundEndEvent());
				}

				Update_CheckBattleResult();
				break;
			case BattleState.WaveEnd:
				break;
		}
	}

	private void NextRound()
	{
		ChangeState(BattleState.WaveEnd);
		curRound++;
		ChangeState(BattleState.WaveStart);
	}

	private void StopBattle()
	{
		Debug.Log("StopBattle");
		ChangeState(BattleState.None);
	}

	public void ClearBattle()
	{
		FireCount = 0;
		UserRoundTimes = 0;
		FireRecovery = 0;
		ChangeFire(0);
		foreach (var c in characers)
		{
			c.Succide();
		}
	}

	private void ChangeState(BattleState state)
	{
		this.state = state;
		timer = 0f;
		if (state == BattleState.WaveStart)
		{
			monsterDeadCount = 0;
			if (curRound == 0)
			{
				CreateCreateshikigamis(shikigamis);
			}
			CreateMonsters(monsters[curRound]);
			BattleProgress.instance.Reset();
			EventManager.instance.FireEvent(new WaveStartEvent());
		}

		if (state == BattleState.WaveEnd)
		{
			
		}
	}

	private void Update_CheckBattleResult()
	{
		//胜利 = 怪物全部死亡，且是最终回合
		//失败 = 己方全部死亡
		//先判断胜利，最后一回合同归于尽也算胜利

		bool isWaveEnd = monsterDeadCount == monsters[curRound].Length;
		
		//胜利
		if (isWaveEnd && curRound == monsters.Count - 1)
		{
			UIManager.instance.OpanelPanel(UIPanelDefineType.BattleResultPanel, true);
			StopBattle();
			return;
		}

		//失败
		if (shikigamiDeadCount == shikigamis.Length)
		{
			UIManager.instance.OpanelPanel(UIPanelDefineType.BattleResultPanel, false);
			StopBattle();
			return;
		}
		
		//当前波次胜利
		if (isWaveEnd)
		{
			NextRound();
		}
	}

	private void ChangeFire(int n)
	{
		FireCount += n;
		if (FireCount < 0)
		{
			FireCount = 0;
		}

		if (FireCount > 8)
		{
			FireCount = 8;
		}
		EventManager.instance.FireEvent(new ChangeFireEvent());
	}

	public void CreateCreateshikigamis(int[] datas)
	{
		for (int i = 0; i < datas.Length; i++)
		{
			var c = ChreateCharacter(datas[i], false);
			c.transform.position = Stage.instance.GetshikigamiPos(datas.Length, i + 1);
			c.transform.localRotation = Quaternion.LookRotation(Vector3.forward);
		}
	}

	public void CreateMonsters(int[] datas)
	{
		for(int i = 0; i < datas.Length; i++)
		{
			var c = ChreateCharacter(datas[i], true);
			c.transform.position = Stage.instance.GetMonsterPos(datas.Length, i + 1);
			c.transform.localRotation = Quaternion.LookRotation(Vector3.back);
		}
	}

	public Characer ChreateCharacter(int dataId, bool isMonster)
	{
		var data = CharacterDataManager.instance.Get(dataId);
		if(data == null)
		{
			Debug.LogErrorFormat("config id = {0} is not found", dataId);
			return null;
		}

		var prefab = isMonster ? Game.instance.monsterPrefab : Game.instance.characterPrefab;
		var go = GameObject.Instantiate(prefab);
		var charcter = go.AddComponent<Characer>();
		charcter.Init(data, isMonster);
		AddCharacter(charcter);
		return charcter;
	}

	public void AddCharacter(Characer c)
	{
		characers.Add(c);
		EventManager.instance.FireEvent(new CharacterBornEvent(c));
	}

	public void RemoveCharacter(Characer c)
	{
		if (c.isMonster)
		{
			monsterDeadCount++;
		}
		else
		{
			shikigamiDeadCount++;
		}
		characers.Remove(c);
	}
}
