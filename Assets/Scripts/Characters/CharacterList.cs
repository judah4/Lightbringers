using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    private static CharacterList _instance;

    public static CharacterList Instance { get {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<CharacterList>("CharacterList"));
        }
        return _instance;
    } }

    public List<AnimatedCharacter> Characters;

    public void Awake()
    {
        _instance = this;
    }

    public AnimatedCharacter Load(int id, Transform parent)
    {
        var prefab = Characters[id];
        var model = Instantiate(prefab, parent.position, parent.rotation, parent);
        return model;
    }
}
