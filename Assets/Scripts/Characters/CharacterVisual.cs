using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject model;

    public void LoadPrimitive(PrimitiveType primitive)
    {
        model = GameObject.CreatePrimitive(primitive);
        model.transform.parent = transform;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

    }

    public void Dead()
    {
        model.SetActive(false);
    }
}
