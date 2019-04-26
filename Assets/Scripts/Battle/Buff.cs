using System;
using System.Collections.Generic;

public class Buff
{
	public Characer caster;
	public Characer receiver;
	public BuffConfig config;
	public bool isFinished;

	private int duration;
	private int runRounds = 0;
	private float attackAdditon;
	private float speedAdditon;

	public Buff(Characer caster, Characer receiver, BuffConfig config, int round)
	{
		this.caster = caster;
		this.receiver = receiver;
		this.config = config;
		this.duration = round;
		Start();
	}

	public void Update()
	{
		if (isFinished)
		{
			return;
		}

		runRounds++;

		if (runRounds == duration)
		{
			Stop();
		}
	}

	public void Start()
	{
		AddEffect();
	}

	public void Stop()
	{
		isFinished = true;
		RemoveEffect();
	}

	private void AddEffect()
	{
		if (config.effectType == BuffEffectType.AddAttack)
		{
			attackAdditon = receiver.attack * config.addAttack;
			receiver.attack += attackAdditon;
		}
		else if (config.effectType == BuffEffectType.AddSpeed)
		{
			speedAdditon = receiver.speed * config.addSpeed;
			receiver.speed += speedAdditon;
		}
		else if(config.effectType == BuffEffectType.PullUp)
		{
			BattleProgress.instance.PullUpCharacterPos(receiver, config.pullUp);
		}
		else if(config.effectType == BuffEffectType.PushDown)
		{
			BattleProgress.instance.PushDownCharacterPos(receiver, -config.pushDown);
		}
	}

	private void RemoveEffect()
	{
		if(config.effectType == BuffEffectType.AddAttack)
		{
			receiver.attack -= attackAdditon;
		}
		else if(config.effectType == BuffEffectType.AddSpeed)
		{
			receiver.speed -= speedAdditon;
		}
		else if(config.effectType == BuffEffectType.PullUp)
		{
		}
		else if(config.effectType == BuffEffectType.PushDown)
		{
		}
	}
}
