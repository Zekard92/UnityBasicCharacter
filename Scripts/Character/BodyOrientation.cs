using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyOrientation : MonoBehaviour
{
    //TODO: Fix Smoothness of the turn.
    void Update()
    {
        Vector2 orientationVector;
        orientationVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float angle = Vector2.Angle(Vector2.up, orientationVector);
        Vector3 crossProduct = Vector3.Cross(Vector2.up, orientationVector);

        if (crossProduct.z > 0)
            angle = 360 - angle;
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            transform.localEulerAngles = Vector3.up * angle;
    }
}
