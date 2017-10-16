

using System;

namespace Assets.Scripts.Encounters.States
{
    public class ActionState : IEncounterState
    {
        public event Action OnKill;
        public event Action OnDamage;
        public CharacterStats Attacker;
        public CharacterStats Target;

        public void Process()
        {
            Target.CurrentHp -= Attacker.Attack;
            OnDamage();
            if (Target.CurrentHp < 0)
            {
                //dead!
                OnKill();
            }
        }

    }
}
