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
	public int normalAttackSkillId;
	public int skillId1;
	public int skillId2;
	public int passiveSkillId;

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
		normalAttackSkillId = json.ReadInt("normalAttackSkillId");
		skillId1 = json.ReadInt("skillId1");
		skillId2 = json.ReadInt("skillId2");
		passiveSkillId = json.ReadInt("passiveSkillId");
	}
}