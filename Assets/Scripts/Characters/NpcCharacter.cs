using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCharacter : MonoBehaviour {

    public int ModelId = 2;

    [SerializeField]
    AnimatedCharacter character;


    void Start()
    {
        character = CharacterList.Instance.Load(ModelId, transform);
    }
	

}
