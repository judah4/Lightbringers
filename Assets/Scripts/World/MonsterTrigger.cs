using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.World
{
    [RequireComponent(typeof(Collider))]
    public class MonsterTrigger : MonoBehaviour
    {
        public float startSize = 0.1f;
        public float EndSize = 1.02f;
        public SphereCollider Collider;
        public int Id = 0;

        public void Awake()
        {
            if(WorldStateManager.Instance.MonsterIds.Contains(Id))
            {
                gameObject.SetActive(false);
            }
        }

        public void Start()
        {
            StartCoroutine(SizeTrigger());
        }

        void OnTriggerEnter(Collider other)
        {
            var chControl = other.gameObject.GetComponent<CharacterController>();
            if (chControl != null)
            {
                VoiceManager.Instance.TriggerVoice();
                WorldStateManager.Instance.MonsterIds.Add(Id);
                SceneManager.LoadScene("BattleInterface");
            }
        }

        IEnumerator SizeTrigger()
        {
            var start = Time.time;
            var end = start + 1;
            while(Time.time < end)
            {
                Collider.radius = Mathf.Lerp(startSize, EndSize, (Time.time-start)/(end - start));
                yield return 0;
            }
            Collider.radius = EndSize;
        }
    }
}
