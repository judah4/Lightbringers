using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : MonoBehaviour {

    private int modelNumber;

    [SerializeField]
    CharacterController characterController;
    AnimatedCharacter character;


    void Start()
    {
        modelNumber = 1;

        character = CharacterList.Instance.Load(modelNumber, transform);
    }

    void ModelSwitch()
    {
        modelNumber = modelNumber % 4 + 1;
        Destroy(character.gameObject);
        character = CharacterList.Instance.Load(modelNumber, transform);

    }

    void Update()
    {
        var vel = characterController.Velocity;
        vel.y = 0;
        if(vel.sqrMagnitude >  0.000001)
        {
            character.transform.forward = vel;
        }
        character.SetMovement(vel.magnitude);
        if (Input.GetButtonDown("Cycle"))
        {
            ModelSwitch();
            print("Switched to Model " + modelNumber);
        }

    }
}
