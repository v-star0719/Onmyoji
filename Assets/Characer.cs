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
	private float maxHp;

	private Skill skill;

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
		hp = d.hp + config.hp;
		maxHp = hp;

		skill = SkillManager.instance.CreateSkill(config.skillId, this);
		animation = GetComponentInChildren<Animation>();
	}

	public void DoAttack()
	{
		skill.Cast(CharacerSearch.SearchTarget(this, TargetFieltType.HpPercentLowest)[0]);
		animation.Play("attack");
	}
	
	public float BeAttacked(float damage)
	{
		var orgHp = hp;
		hp -= damage;
		if (hp < 0)
		{
			hp = 0;
		}

		EventManager.instance.FireEvent(new DamageEvent(null, this, damage, false, false));
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
