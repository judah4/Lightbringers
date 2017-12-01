using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Items
{
	public class HealEffect : TargetedEffect
	{
		int strength;

		public void Heal()
		{
			if(target.CurrentHp + strength > target.MaxHp)
			{
				target.CurrentHp = target.MaxHp;
			}
			else
			{
				target.CurrentHp += strength;
			}
		}
	}
}
