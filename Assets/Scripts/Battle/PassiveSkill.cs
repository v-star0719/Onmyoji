using System.Collections;
using Kernel;

public class PassiveSkill
{
	public Characer caster;
	public PassiveSkillConfig config;
	public Skill skill;
	public PassiveSkill(int skillId, Characer caster)
	{
		this.caster = caster;
		config = PassiveSkillConfigManager.instance.Get(skillId);
		skill = SkillManager.instance.CreateSkill(config.skillId, this.caster);
	}

	public void Update()
	{

	}

	public void Start()
	{
		EventManager.instance.SubscribeEvent((int)EventType.WaveStart, OnWaveStart);
	}

	public void Stop()
	{
		EventManager.instance.UnsubscribeEvent((int)EventType.WaveStart, OnWaveStart);
	}

	public void OnWaveStart(Event e)
	{
		if (config.eventType == PassiveSkillEventType.WaveStart)
		{
			var target = CharacerSearch.SearchTarget(caster, skill.config.targetType, skill.config.targetFieltType);
			foreach (Characer characer in target)
			{
				skill.Cast(characer);
			}
		}
	}
}
