using UnityEngine;

public class DamageInfo
{
	public bool isHeal;
	public bool isCrit;
	public float damage;

	public DamageInfo()
	{
	}

	public DamageInfo(DamageInfo info)
	{
		isHeal = info.isHeal;
		isCrit = info.isCrit;
		damage = info.damage;
	}
}

public class DamageUtil
{
	//正常伤害计算
	public static DamageInfo CalculateDamage(Characer caster, Characer receiver, Skill skill)
	{
		DamageInfo damgeInfo = new DamageInfo();
		damgeInfo.damage = skill.config.damageRate * caster.attack;
		damgeInfo.isCrit = Random.Range(0, 100) < caster.attack;
		if (damgeInfo.isCrit)
		{
			damgeInfo.damage = damgeInfo.damage * caster.critDamge;
		}

		float defend = 300 / (receiver.defense + 300);
		damgeInfo.damage *= defend;

		DamageApplyCharacterSpeciality(caster, receiver, damgeInfo);

		return damgeInfo;
	}

	//不计算防御暴击的伤害
	public static DamageInfo CalculateDamage(Characer caster, Characer receiver, float damage)
	{
		DamageInfo damgeInfo = new DamageInfo();
		damgeInfo.damage = damage;
		DamageApplyCharacterSpeciality(caster, receiver, damgeInfo);
		return damgeInfo;
	}

	private static void DamageApplyCharacterSpeciality(Characer caster, Characer receiver, DamageInfo damgeInfo)
	{
		if(caster.special == CharacterSpecialType.HighPercentHpDamageAdd)
		{
			if(receiver.GetHp(true) > 0.7f)
			{
				damgeInfo.damage *= 1.4f;
			}
		}
	}
}