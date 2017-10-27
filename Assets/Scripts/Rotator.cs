using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float x = 0;
    public float y = 20;
    public float z = 10;

	// Update is called once per frame
	void Update ()
    {
        //rotates along selected axis
        //larger the number, faster the rotation
        transform.Rotate(new Vector3(0 , 0, 0) * Time.deltaTime);
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
    }
}
