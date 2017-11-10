﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Characters.MonsterTemplates;
using UnityEngine;
using System;

[Serializable]
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
    public int ExpNeeded = 100;

    public CharacterClass CharacterClass = CharacterClass.Classless;
    private CharacterClass characterClassChange = CharacterClass.Classless;

    public bool Player = false;
    public bool Dead {get { return CurrentHp < 1; }}

    [SerializeField]
    public CharacterVisual CharacterVisual;

    public event Action<int> OnHealthChange;
    public event Action<int> OnManaChange;
    public event Action<int, int> OnExp;
    public event Action<int> OnLevel;
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

    void SetLevel(int newLevel)
    {
        Level = newLevel;
        CalcStats();
        if(OnLevel != null)
        {
            OnLevel(Level);
        }
    }

    void CalcStats()
    {
        if(CharacterClass == CharacterClass.Classless)
            return;

        var calcer = new StatCalcer(CharacterClass);

        Hp = calcer.GetHp(Level);
        CurrentHp = Hp;
        Mana = calcer.GetMana(Level);
        CurrentMana = Mana;
        Attack = calcer.GetAttack(Level);
        Defense = calcer.GetDefense(Level);
        Speed = calcer.GetSpeed(Level);
        ExpNeeded = ExpNeededCalc(Level);

        if(OnHealthChange != null)
        {
            OnHealthChange(CurrentHp);
        }
    }

    public void Setup(MonsterData monsterData)
    {
        CharacterClass = CharacterClass.Classless;
        name = monsterData.name;
        Hp = monsterData.hp;
        CurrentHp = Hp;
        Mana = monsterData.mana;
        CurrentMana = Mana;

        Level = monsterData.level;

        Attack = monsterData.attack;
        Defense = monsterData.defense;
        Speed = monsterData.speed;

        Exp = monsterData.exp;
    }

    public void SetClass(CharacterClass characterClass)
    {
        CharacterClass = characterClass;
        switch (CharacterClass)
        {
            case CharacterClass.Warrior:
                name = "Cecil";
                break;
            case CharacterClass.Wizard:
                name = "Laura";
                break;
            case CharacterClass.Cleric:
                name = "Vestele";
                break;
            case CharacterClass.Rogue:
                name = "Rick";
                break;
                
        }

        CalcStats();
    }

    public void Damage(int damage)
    {
        CurrentHp -= damage;
        if(OnHealthChange != null)
        {
            OnHealthChange(CurrentHp);
        }
    }

    public void GiveExp(int earned)
    {
        Exp += earned;
        if(OnExp != null)
        {
            OnExp(Exp, earned);
        }

        if(Exp >= ExpNeeded)
        {
            SetLevel(Level+1);
            

            Exp -= ExpNeeded;
            if(OnExp != null)
            {
                OnExp(Exp, earned);
            }
        }
    }

    public static int ExpNeededCalc(int level)
    {
        return level * 100;
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
