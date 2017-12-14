using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Encounters;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BattleInterface : MonoBehaviour
    {
        [SerializeField]
        private BasicEncounterSetup encounter;

        public List<Button> AttackButtons;
        public List<Button> MoveButtons;
        public List<Button> ItemButtons;
        public List<Health> HealthBars;
        public List<Text> Names;
        public Text TopName;

        public GameObject BackButton;
        public GameObject AttackPanel;
        public GameObject ActionsPanel;
        public GameObject MovePanel;
        public GameObject ItemPanel;

        // Use this for initialization
        void Start () {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            for (int cnt = 0; cnt < encounter.Players.Count; cnt++)
            {
                var player = encounter.Players[cnt];
                Names[cnt].text = player.name;
                HealthBars[cnt].SetHealth(player.CurrentHp, player.MaxHp);
            }

            encounter.OnTurn += () =>
            {
                BackButton.SetActive(false);
                AttackPanel.SetActive(false);
                ActionsPanel.SetActive(true);
                MovePanel.SetActive(false);
                ItemPanel.SetActive(false);
            };

            encounter.OnEncounterState += Encounter_OnEncounterState;
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
                HealthBars[cnt].SetHealth(player.CurrentHp, player.MaxHp);
            }

            for (int cnt = 0; cnt < AttackButtons.Count; cnt++)
            {
                if (cnt >= encounter.Monsters.Count)
                {
                    AttackButtons[cnt].gameObject.SetActive(false);
                    continue;
                }

                AttackButtons[cnt].gameObject.SetActive(encounter.Monsters[cnt] != null && !encounter.Monsters[cnt].Dead);

            }

            for (int cnt = 0; cnt < MoveButtons.Count; cnt++)
            {
                if (cnt >= encounter.Monsters.Count)
                {
                    MoveButtons[cnt].gameObject.SetActive(false);
                    continue;
                }

                MoveButtons[cnt].gameObject.SetActive(encounter.Monsters[cnt] != null && !encounter.Monsters[cnt].Dead);

            }

            for (int cnt = 0; cnt < ItemButtons.Count; cnt++)
            {
                if (cnt >= encounter.Monsters.Count)
                {
                    ItemButtons[cnt].gameObject.SetActive(false);
                    continue;
                }

                ItemButtons[cnt].gameObject.SetActive(encounter.Monsters[cnt] != null && !encounter.Monsters[cnt].Dead);

            }
        }

        private void Encounter_OnEncounterState(BasicEncounterSetup.EncounterStates state)
        {
            if (state == BasicEncounterSetup.EncounterStates.Results)
            {
                StartCoroutine(ResultsThenLeave());
            }
        }

        IEnumerator ResultsThenLeave()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(WorldStateManager.Instance.CurrentScene);
        }
    }
}
