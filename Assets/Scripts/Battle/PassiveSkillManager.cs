using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillManager : MonoBehaviour
{
	public static PassiveSkillManager instance;

	public List<PassiveSkill> skills = new List<PassiveSkill>();

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		foreach (var skill in skills)
		{
			skill.Update();
		}
	}

	public PassiveSkill CreateSkill(int skillId, Characer caster)
	{
		var skill = new PassiveSkill(skillId, caster);
		skill.Start();
		skills.Add(skill);
		return skill;
	}

	public void RemoveSkill(PassiveSkill skill)
	{
		skill.Stop();
		skills.Remove(skill);
	}

	public void ClearAll()
	{
		foreach (var skill in skills)
		{
			skill.Stop();
		}
		skills.Clear();
	}
}