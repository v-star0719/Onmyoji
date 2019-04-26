using System.Collections.Generic;

public class BuffManager
{
	public static BuffManager instance = new BuffManager();

	private List<Buff> buffs = new List<Buff>();

	//update per round
	public void Round()
	{
		foreach (var buff in buffs)
		{
			buff.Update();
		}
	}

	public void RoundStart()
	{

	}

	public void RoundEnd()
	{
		for (var i = 0; i < buffs.Count; i++)
		{
			var buff = buffs[i];
			if (buff.isFinished)
			{
				i--;
				RemoveBuff(buff);
			}
		}
	}

	public void AddBuff(int id, Characer caster, Characer receiver, int duration)
	{
		var data = BuffConfigManager.instance.Get(id);
		Buff buff = new Buff(caster, receiver, data, duration);
		buffs.Add(buff);
		receiver.buffs.Add(buff);
	}

	public void RemoveBuff(Buff buff)
	{
		if (!buff.isFinished)
		{
			buff.Stop();
		}
		buffs.Remove(buff);
		buff.receiver.buffs.Add(buff);
	}
}