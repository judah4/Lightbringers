using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalcAttribute {

	public int Amount { get; set; }
    public bool PerLevel { get; set; }

    public StatCalcAttribute(int amount, bool perLevel)
    {
        Amount = amount;
        PerLevel = perLevel;
    }
}
