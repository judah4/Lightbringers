using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public static CharacterList Instance;

    public List<GameObject> Characters;

    public void Awake()
    {
        Instance = this;
    }

    public GameObject Load(int id, Transform parent)
    {
        var prefab = Characters[id];
        var model = Instantiate(prefab, parent.position, parent.rotation, parent);
        return model;
    }
}
