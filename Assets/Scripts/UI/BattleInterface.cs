using System.Collections.Generic;
using Assets.Scripts.Encounters;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BattleInterface : MonoBehaviour
    {
        [SerializeField]
        private BasicEncounterSetup encounter;

        public List<Button> AttackButtons;
        public List<Health> HealthBars;
        public List<Text> Names;
        public Text TopName;

        public GameObject AttackPanel;
        public GameObject ActionsPanel;

        // Use this for initialization
        void Start () {
            for (int cnt = 0; cnt < encounter.Players.Count; cnt++)
            {
                var player = encounter.Players[cnt];
                Names[cnt].text = player.name;
                HealthBars[cnt].SetHealth(player.CurrentHp, player.Hp);
            }

            encounter.OnTurn += () =>
            {
                AttackPanel.SetActive(false);
                ActionsPanel.SetActive(true);
            };
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (encounter.CharacterTurn != null)
            {
                TopName.text = encounter.CharacterTurn.Character.name;
            }
            //super lazy
            //todo, make event based
            for (int cnt = 0; cnt < encounter.Players.Count; cnt++)
            {
                var player = encounter.Players[cnt];
                HealthBars[cnt].SetHealth(player.CurrentHp, player.Hp);
            }

            for (int cnt = 0; cnt < AttackButtons.Count; cnt++)
            {
                if (cnt >= encounter.Monsters.Count)
                {
                    AttackButtons[cnt].gameObject.SetActive(false);
                    continue;
                }

                AttackButtons[cnt].gameObject.SetActive(!encounter.Monsters[cnt].Dead);

            }
        }
    }
}
