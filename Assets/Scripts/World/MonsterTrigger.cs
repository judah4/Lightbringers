using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.World
{
    [RequireComponent(typeof(Collider))]
    public class MonsterTrigger : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            var chControl = other.gameObject.GetComponent<CharacterController>();
            if (chControl != null)
            {
                VoiceManager.Instance.TriggerVoice();
                SceneManager.LoadScene("BattleInterface");
            }
        }
    }
}
