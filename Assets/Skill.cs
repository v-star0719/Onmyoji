using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill
{
	public SkillConfig config;
	public bool isCasted = false;
	public Characer caster;

	private int attackTimes;
	private float attackTimer;
	private Characer target;

	public void Init(Characer c, SkillConfig config)
	{
		caster = c;
		this.config = config;
	}

	public void Update()
	{
		if (!isCasted)
		{
			return;
		}

		attackTimer -= Time.deltaTime;
		if (attackTimer <= 0)
		{
			attackTimer = config.attackInterval;
			List<Characer> targets = new List<Characer>();
			if (config.isAoe)
			{
				targets = CharacerSearch.SearchTarget(caster, TargetFieltType.None);
			}
			else
			{
				targets.Add(target);
			}
			
			foreach(var t in targets)
			{
				DoDamage(t);
			}

			attackTimes++;
		}

		int maxTimes = config.attackTimes;
		if (attackTimes >= maxTimes)
		{
			isCasted = false;
		}
	}

	public void Cast(Characer target)
	{
		isCasted = true;
		attackTimes = 0;
		attackTimer = -1;//第一次立即执行
		this.target = target;

		if (config.buff1Id > 0)
		{
			BuffManager.instance.AddBuff(config.buff1Id, caster, target, config.buff1Round);
		}
		if(config.buff2Id > 0)
		{
			BuffManager.instance.AddBuff(config.buff2Id, caster, target, config.buff2Round);
		}
	}

	public void DoDamage(Characer receiver)
	{
		float d = config.damageRate * caster.attack;
		float damage = receiver.BeAttacked(d);

		if (config.onePounch)
		{
			float overflow = d - damage;
			if (d - damage > 1)
			{
				var targets = CharacerSearch.SearchTarget(caster, TargetFieltType.None);
				foreach (var t in targets)
				{
					if (t != receiver)
					{
						t.BeAttacked(overflow);
					}
				}
			}
		}

	}
}
