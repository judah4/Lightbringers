using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField]
    private AnimatedCharacter model;

    public int Id;

    //public void LoadPrimitive(PrimitiveType primitive)
    //{
    //    model = GameObject.CreatePrimitive(primitive);
    //    model.transform.parent = transform;
    //    model.transform.localPosition = Vector3.zero;
    //    model.transform.localRotation = Quaternion.identity;

    //}

    public void LoadModel(int id)
    {
        Id = id;
        model = CharacterList.Instance.Load(id, transform);
    }

    public void Dead()
    {
        model.Die();
    }
}
