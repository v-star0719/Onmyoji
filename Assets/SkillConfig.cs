using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public enum TargetType
{
	Enemy = 0,
	Partener = 1,
	Self = 2,
}

public class SkillConfig
{
	public int id;
	public string name;
	public bool isAoe;
	public TargetType targetType;
	public TargetFieltType targetFieltType;
	//public bool attackSelectedTarget;//单体攻击的情况下
	public int attackTimes;
	public float attackInterval;
	public float damageRate;
	public bool onePunch;
	public int buff1Id;
	public int buff1Round;
	public int buff2Id;
	public int buff2Round;
	public int addFire;

	public void ReadFromJson(JsonData json)
	{
		id = json.ReadInt("id");
		name = json.ReadString("name");
		isAoe = json.ReadBoolean("isAoe");
		targetType = (TargetType)json.ReadInt("targetType");
		targetFieltType = (TargetFieltType)json.ReadInt("targetFieltType");
		attackTimes = json.ReadInt("attackTimes");
		attackInterval = json.ReadFloat("attackInterval");
		damageRate = json.ReadFloat("damageRate");
		onePunch = json.ReadBoolean("onePunch");
		buff1Id = json.ReadInt("buff1Id");
		buff1Round = json.ReadInt("buff1Round");
		buff2Id = json.ReadInt("buff2Id");
		buff2Round = json.ReadInt("buff2Round");
		addFire = json.ReadInt("addFire");
	}
}
