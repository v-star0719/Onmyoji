using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

//class Singleton<T>
//{
//	//public static T Instance = 
//}

public class SkillManager : MonoBehaviour
{
	public static SkillManager instance;

	private List<Skill> skills = new List<Skill>();

	//private List<Skill> castedSkills = new List<Skill>();
	// Use this for initialization

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		for (var index = 0; index < skills.Count; index++)
		{
			var skill = skills[index];
			if (skill.isCasted)
			{
				skill.Update();
			}
		}
	}

	public Skill CreateSkill(int skillId, Characer c)
	{
		Skill s = new Skill();
		s.Init(c, SkillConfigManager.instance.Get(skillId));
		skills.Add(s);
		return s;
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
