using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int Hp;
    public int CurrentHp;
    public int Mana;
    public int CurrentMana;

    public int Attack;
    public int Defense;

    public int Speed;

    public int Level = 1;
    private int levelChange = 1;
    public int Exp = 0;
    public int ExpNeeded = 500;

    public CharacterClass CharacterClass = CharacterClass.Classless;
    private CharacterClass characterClassChange = CharacterClass.Classless;

    public void Start()
    {
        CalcStats();
    }

    void Update()
    {
        if (characterClassChange != CharacterClass)
        {
            CalcStats();
            characterClassChange = CharacterClass;
        }

        if (levelChange != Level)
        {
            CalcStats();
            levelChange = Level;
        }
    }

    void CalcStats()
    {
        if(CharacterClass == CharacterClass.Classless)
            return;

        var calcer = new StatCalcer(CharacterClass);

        Hp = calcer.GetHp(Level);
        Mana = calcer.GetMana(Level);
        Attack = calcer.GetAttack(Level);
        Defense = calcer.GetDefense(Level);
        Speed = calcer.GetSpeed(Level);
    }

}

public enum CharacterClass
{
    Classless = 0,
    Warrior = 1,
    Wizard,
    Cleric,
    Rogue
}
