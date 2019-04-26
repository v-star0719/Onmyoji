using LitJson;

public class PassiveSkillConfig
{
	public int id;
	public string name;
	public PassiveSkillEventType eventType;
	public int skillId;

	public void ReadFromJson(JsonData json)
	{
		id = json.ReadInt("id");
		name = json.ReadString("name");
		eventType = (PassiveSkillEventType)json.ReadInt("eventType");
		skillId = json.ReadInt("skillId");
	}
}
