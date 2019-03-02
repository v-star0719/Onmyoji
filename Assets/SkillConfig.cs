using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public enum TargetType
{
	Enemy,
	Partener,
}

public class SkillConfig
{
	public int id;
	public string name;
	public bool isAoe;
	public TargetType targetType;
	//public bool attackSelectedTarget;//单体攻击的情况下
	public int attackTimes;
	public float attackInterval;
	public float damageRate;
	public bool onePounch;
	public int buff1Id;
	public int buff1Round;
	public int buff2Id;
	public int buff2Round;

	public void ReadFromJson(JsonData json)
	{
		id = json.ReadInt("id");
		name = json.ReadString("name");
		isAoe = json.ReadBoolean("isAoe");
		targetType = (TargetType)json.ReadInt("targetType");
		attackTimes = json.ReadInt("attackTimes");
		attackInterval = json.ReadFloat("attackInterval");
		damageRate = json.ReadFloat("damageRate");
		onePounch = json.ReadBoolean("onePounch");
		buff1Id = json.ReadInt("buff1Id");
		buff1Round = json.ReadInt("buff1Round");
		buff2Id = json.ReadInt("buff2Id");
		buff2Round = json.ReadInt("buff2Round");
	}
}
