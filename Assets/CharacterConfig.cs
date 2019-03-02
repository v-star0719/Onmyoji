using LitJson;

public class CharacterConfig
{
	public int id;
	public string name;
	public int hp;
	public int attack;
	public int defense;
	public int crit;//百分之n
	public int critDamage;//百分之n
	public int speed;
	public int skillId;

	public void ReadFromJson(JsonData json)
	{
		id = json.ReadInt("id");
		name = json.ReadString("name");
		hp = json.ReadInt("hp");
		attack = json.ReadInt("attack");
		defense = json.ReadInt("defense");
		crit = json.ReadInt("crit");
		critDamage = json.ReadInt("critDamage");
		speed = json.ReadInt("speed");
		skillId = json.ReadInt("skillId");
	}
}