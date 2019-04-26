using System.Collections;
using System.Collections.Generic;
using Kernel;
using UnityEngine;

public class Characer : MonoBehaviour
{
	public bool isMonster = false;

	private CharacterData data;
	private CharacterConfig config;
	public float speed;
	public float attack;
	private float hp;
	public float maxHp;
	public float crit;
	public float critDamge;
	public float defense;
	public CharacterSpecialType special;


	private Skill skill;
	private Skill normalAttackSkill;
	private PassiveSkill passiveSkill;

	private new Animation animation;

	public bool isDying;
	private float dyingTime;
	public List<Buff> buffs = new List<Buff>();

	public bool isDestoryed = false;

	public string Name
	{
		get { return data.name; }
	}

	public bool IsCastingSkill
	{
		get { return !skill.isCasted; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDying)
		{
			dyingTime += Time.deltaTime;
			if (dyingTime >= 0.7f)
			{
				Dead();
			}
		}
	}

	public void Init(CharacterData d, bool monster)
	{
		config = CharacterConfigManager.instance.Get(d.configId);
		data = d;
		isMonster = monster;
		speed = d.speed + config.speed;
		attack = d.attack + config.attack;
		crit = d.crit + config.crit;
		critDamge = (d.critDamage + config.critDamage) / 100f;
		defense = config.defense;
		hp = d.hp + config.hp;
		special = d.specialType;
		maxHp = hp;

		if (config.skillId1 > 0)
		{
			skill = SkillManager.instance.CreateSkill(config.skillId1, this);
		}
		if (config.passiveSkillId > 0)
		{
			passiveSkill = PassiveSkillManager.instance.CreateSkill(config.passiveSkillId, this);
		}
		normalAttackSkill = SkillManager.instance.CreateSkill(config.normalAttackSkillId, this);

		animation = GetComponentInChildren<Animation>();
	}

	public void DoAttack()
	{
		if (skill != null)
		{
			skill.Cast(CharacerSearch.SearchTarget(this, TargetType.Enemy, TargetFieltType.HpPercentLowest)[0]);
		}
		else
		{
			
		}
		animation.Play("attack");
	}
	
	public float BeAttacked(DamageInfo damageInfo)
	{
		var orgHp = hp;
		hp -= damageInfo.damage;
		if (hp < 0)
		{
			hp = 0;
		}

		EventManager.instance.FireEvent(new DamageEvent(null, this, damageInfo));
		animation.Play("hit");

		if(hp <= 0)
		{
			Dying();
		}

		return orgHp - hp;
	}

	private void Dying()
	{
		isDying = true;
	}

	private void Dead()
	{
		BattleManager.instance.RemoveCharacter(this);
		EventManager.instance.FireEvent(new CharacterDeadEvent(this));

		foreach(Buff buff in buffs)
		{
			BuffManager.instance.RemoveBuff(buff);
		}
		buffs.Clear();

		isDestoryed = true;
		GameObject.Destroy(gameObject);
	}

	public float GetHp(bool isPercent)
	{
		if (isPercent)
		{
			return hp / maxHp;
		}
		else
		{
			return hp;
		}
	}

	public void Succide()
	{
		isDying = true;
	}
}
