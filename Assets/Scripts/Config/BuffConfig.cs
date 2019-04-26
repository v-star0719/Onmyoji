using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public enum BuffEffectType
{
    None,
    AddAttack = 1,
    AddSpeed = 2,
    PullUp = 3,
    PushDown = 4,
}

public class BuffConfig
{
    public int id;
	public string name;
    public BuffEffectType effectType;
    public int addAttack;//百分之几
	public int addSpeed;//速度值
	public int pullUp;//百分之几
    public int pushDown;//百分之几

	public void ReadFromJson(JsonData json)
	{
		id = json.ReadInt("id");
		name = json.ReadString("name");
		effectType = (BuffEffectType)json.ReadInt("effectType");
		addAttack = json.ReadInt("addAttack");
		addSpeed = json.ReadInt("addSpeed");
		pullUp = json.ReadInt("pullUp");
		pushDown = json.ReadInt("pushDown");
	}
}
