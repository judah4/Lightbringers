using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalcer
{
    private CharacterClass _characterClass;

    

    public StatCalcer(CharacterClass characterClass)
    {
        _characterClass = characterClass;

    }

    public int GetHp(int level)
    {
        var stats = new List<StatCalcAttribute>();
        stats.Add(new StatCalcAttribute(10, false));
        switch (_characterClass)
        {
                case CharacterClass.Warrior:
                stats.Add(new StatCalcAttribute(5, true));
                break;
                case CharacterClass.Wizard:
                stats.Add(new StatCalcAttribute(2, true));
                break;
                case CharacterClass.Cleric:
                stats.Add(new StatCalcAttribute(2, true));
                break;
                case CharacterClass.Rogue:
                stats.Add(new StatCalcAttribute(3, true));
                break;
        }

        return Calc(stats, level);
    }

    public int GetMana(int level)
    {
        var stats = new List<StatCalcAttribute>();
        switch (_characterClass)
        {
                case CharacterClass.Warrior:
                stats.Add(new StatCalcAttribute(2, true));
                break;
                case CharacterClass.Wizard:
                stats.Add(new StatCalcAttribute(8, true));
                break;
                case CharacterClass.Cleric:
                stats.Add(new StatCalcAttribute(6, true));
                break;
                case CharacterClass.Rogue:
                stats.Add(new StatCalcAttribute(4, true));
                break;
        }

        return Calc(stats, level);
    }

    public int GetAttack(int level)
    {
        var stats = new List<StatCalcAttribute>();
        switch (_characterClass)
        {
                case CharacterClass.Warrior:
                stats.Add(new StatCalcAttribute(3, true));
                break;
                case CharacterClass.Wizard:
                stats.Add(new StatCalcAttribute(2, true));
                break;
                case CharacterClass.Cleric:
                stats.Add(new StatCalcAttribute(1, true));
                break;
                case CharacterClass.Rogue:
                stats.Add(new StatCalcAttribute(2, true));
                break;
        }

        return Calc(stats, level);
    }

    public int GetDefense(int level)
    {
        var stats = new List<StatCalcAttribute>();
        switch (_characterClass)
        {
                case CharacterClass.Warrior:
                stats.Add(new StatCalcAttribute(2, true));
                break;
                case CharacterClass.Wizard:
                stats.Add(new StatCalcAttribute(1, true));
                break;
                case CharacterClass.Cleric:
                stats.Add(new StatCalcAttribute(1, true));
                break;
                case CharacterClass.Rogue:
                stats.Add(new StatCalcAttribute(1, true));
                break;
        }

        return Calc(stats, level);
    }

    public int GetSpeed(int level)
    {
        var stats = new List<StatCalcAttribute>();
        switch (_characterClass)
        {
                case CharacterClass.Warrior:
                stats.Add(new StatCalcAttribute(1, true));
                break;
                case CharacterClass.Wizard:
                stats.Add(new StatCalcAttribute(2, true));
                break;
                case CharacterClass.Cleric:
                stats.Add(new StatCalcAttribute(2, true));
                break;
                case CharacterClass.Rogue:
                stats.Add(new StatCalcAttribute(4, true));
                break;
        }

        return Calc(stats, level);
    }

    //calcs from the list of stats
    int Calc(List<StatCalcAttribute> stats, int level)
    {
        var total = 0;
        for (int cnt = 0; cnt < stats.Count; cnt++)
        {
            var curLevel = level;
            if (!stats[cnt].PerLevel)
                curLevel = 1;
            total += stats[cnt].Amount * curLevel;
        }
        return total;

    }
    
}

