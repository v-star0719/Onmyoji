using JetBrains.Annotations;
using Kernel;

public enum EventType
{
	CharacterBorn,
	CharacterDead,
	OnDamage,
	RoundStart,
	AddBuff,
	RemoveBuff,
}

public class CharacterBornEvent : Event
{
	public Characer characer;

	public CharacterBornEvent(Characer c)
	{
		id = (int)EventType.CharacterBorn;
		characer = c;
	}
}

public class CharacterDeadEvent : Event
{
	public Characer characer;

	public CharacterDeadEvent(Characer c)
	{
		id = (int)EventType.CharacterDead;
		characer = c;
	}
}

public class DamageEvent : Event
{
	public Characer caster;
	public Characer receiver;
	public float damageValue;
	public bool isCrit;
	public bool isHeal;

	public DamageEvent(Characer caster, Characer receiver, float damage, bool isCrit, bool isHeal)
	{
		id = (int)EventType.OnDamage;
		this.caster = caster;
		this.receiver = receiver;
		this.damageValue = damage;
		this.isCrit = isCrit;
		this.isHeal = isHeal;
	}
}

public class RoundStartEvent : Event
{
	public RoundStartEvent()
	{
		id = (int)EventType.RoundStart;
	}
}

public class AddBuffEvent : Event
{
	public Characer characer;
	public Buff buff;
	public AddBuffEvent(Characer characer, Buff buff)
	{
		id = (int)EventType.AddBuff;
		this.characer = characer;
		this.buff = buff;
	}
}

public class RemoveBuffEvent : Event
{
	public Characer characer;
	public Buff buff;
	public RemoveBuffEvent(Characer characer, Buff buff)
	{
		id = (int)EventType.RemoveBuff;
		this.characer = characer;
		this.buff = buff;
	}
}