using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Characters.MonsterTemplates;
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

    public bool Player = false;
    public bool Dead {get { return CurrentHp < 1; }}

    [SerializeField]
    public CharacterVisual CharacterVisual;

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
        CurrentHp = Hp;
        Mana = calcer.GetMana(Level);
        CurrentMana = Mana;
        Attack = calcer.GetAttack(Level);
        Defense = calcer.GetDefense(Level);
        Speed = calcer.GetSpeed(Level);
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
}

public enum CharacterClass
{
    Classless = 0,
    Warrior = 1,
    Wizard,
    Cleric,
    Rogue
}
