using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetFieltType
{
	None,
	HpPercentLowest,
}

public class CharacerSearch
{
	public static List<Characer> SearchTarget(Characer characer, TargetFieltType fieltType)
	{
		List<Characer> targets = new List<Characer>();

		var list = BattleManager.instance.characers;
		foreach(var c in list)
		{
			if(c.isDying)
			{
				continue;
			}

			if(IsTarget(characer, c))
			{
				targets.Add(c);
			}
		}

		if(fieltType == TargetFieltType.None)
		{
			return targets;
		}

		if (fieltType == TargetFieltType.HpPercentLowest)
		{
			Characer t = null;
			float hp = 1;
			foreach(var c in targets)
			{
				if(t == null || c.GetHp(true) <= hp)
				{
					hp = c.GetHp(true);
					t = c;
				}
			}
			targets.Clear();
			targets.Add(t);
		}
		
		return targets;
	}

	private static bool IsTarget(Characer c, Characer target)
	{
		return (c.isMonster != target.isMonster);
	}
}
