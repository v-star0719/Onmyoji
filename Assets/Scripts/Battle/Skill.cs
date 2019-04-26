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

	private float overflowDamage = 0;

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
				targets = CharacerSearch.SearchTarget(caster, config.targetType, TargetFieltType.None);
			}
			else
			{
				targets.Add(target);
			}
			
			foreach(var t in targets)
			{
				DoAttack(t);
			}

			attackTimes++;
		}

		int maxTimes = config.attackTimes;
		if (attackTimes >= maxTimes)
		{
			isCasted = false;

			if(config.onePunch)
			{
				if(overflowDamage > 1)
				{
					var targets = CharacerSearch.SearchTarget(caster, TargetType.Enemy, TargetFieltType.None);
					foreach(var t in targets)
					{
						var damageInfo = DamageUtil.CalculateDamage(caster, t, overflowDamage);
						t.BeAttacked(damageInfo);
					}
				}
			}
		}
	}

	public void Cast(Characer target)
	{
		isCasted = true;
		attackTimes = 0;
		attackTimer = -1;//第一次立即执行
		overflowDamage = 0;
		this.target = target;

		if (config.buff1Id > 0)
		{
			BuffManager.instance.AddBuff(config.buff1Id, caster, target, config.buff1Round);
		}
		if(config.buff2Id > 0)
		{
			BuffManager.instance.AddBuff(config.buff2Id, caster, target, config.buff2Round);
		}

		if (config.addFire > 0)
		{
			BattleManager.instance.ChangeFire(config.addFire);
		}
	}

	public void DoAttack(Characer receiver)
	{
		var d = DamageUtil.CalculateDamage(caster, receiver, this);
		float hpChange = receiver.BeAttacked(d);
		overflowDamage += d.damage - hpChange;

		if (caster.special == CharacterSpecialType.RealDamage)
		{
			float damage = caster.attack * 1.2f;
			float max = receiver.maxHp * 0.1f;
			if (damage > max)
			{
				damage = max;
			}
			DamageInfo info = new DamageInfo();
			info.damage = damage;
			receiver.BeAttacked(info);
		}
	}

	public void Stop()
	{
		isCasted = false;
	}
}
