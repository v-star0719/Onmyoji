using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public enum CharacterSpecialType
{
	HighPercentHpDamageAdd = 1, //破势
	LowerPercentHpDamageAdd = 2,//心眼
	RealDamage = 3,//针女
	IgnoreDefense = 4,//网切
}

public class CharacterData
{
	public int id;
	public string name;
	public int configId;
	public int hp;
	public int attack;
	public int crit;//百分之几
	public int critDamage;//百分之几
	public int speed;
	public int defense;
	public CharacterSpecialType specialType;

	public void ReadFromJson(JsonData json)
	{
		id = json.ReadInt("id");
		configId = json.ReadInt("configId");
		name = json.ReadString("name");
		hp = json.ReadInt("hp");
		attack = json.ReadInt("attack");
		crit = json.ReadInt("crit");
		critDamage = json.ReadInt("critDamage");
		speed = json.ReadInt("speed");
		defense = json.ReadInt("defense");
		specialType = (CharacterSpecialType)json.ReadInt("specialType");
	}
}
