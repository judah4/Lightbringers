using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class BattleCryManager : MonoBehaviour {

        private static BattleCryManager _instance;

        public static BattleCryManager Instance { get {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<BattleCryManager>("BattleCryManager"));
            }
            return _instance;
        } }


        public List<CharacterSounds> SoundClips;
        public AudioSource AudioSource;

        public void Awake()
        {
            _instance = this;
        }

        public void Load(CharacterClipType clipType, int id, Vector3 position)
        {
            
            var clips = SoundClips[id];
            var clipList = GetList(clips, clipType);
            AudioClip clip = clipList[Random.Range(0, clipList.Count)];

            AudioSource.clip = clip;//.PlayClipAtPoint(clip, position, 1);
            AudioSource.Play();
        }

        List<AudioClip> GetList(CharacterSounds clips, CharacterClipType clipType)
        {
            if (clipType == CharacterClipType.Attack)
            {
                return clips.Attacks;
            }
            else if (clipType == CharacterClipType.Hurt)
            {
                return clips.Hurts;
            }
            else if (clipType == CharacterClipType.Death)
            {
                return clips.Attacks;
            }
            return clips.Deaths;
        }
    }

    public enum CharacterClipType
    {
        Attack,
        Hurt,
        Death
    }

    [System.Serializable]
    public class CharacterSounds
    {
        public List<AudioClip> Attacks = new List<AudioClip>();
        public List<AudioClip> Deaths = new List<AudioClip>();
        public List<AudioClip> Hurts = new List<AudioClip>();

    }
}

